import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/_services/admin.service';
import { User } from 'src/app/_models/User';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { RolesModalComponent } from '../roles-modal/roles-modal.component';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {

  users: User[];
  bsModalRef: BsModalRef;

  constructor(private adminService: AdminService, private modalService: BsModalService, private as: AlertifyService) { }

  ngOnInit() {
    this.getUsersWithRoles();
  }

  getUsersWithRoles() {
    this.adminService.getUsersWithRoles().subscribe((users: User[]) => {
      this.users = users;
    }, err =>  this.as.error(err));
  }

  editRolesModal(user: User) {
    const initialState = {
      user,
      roles: this.getRolesArray(user)
    };

  this.bsModalRef = this.modalService.show(RolesModalComponent, { initialState });
  this.bsModalRef.content.updateSelectedRoles.subscribe(roles => {
    const rolesToUpdate = {
      roleNames: roles.filter(r => r.checked === true).map(r => r.name)
    };

    if (rolesToUpdate) {
      this.adminService.updateUserRoles(user.id, rolesToUpdate).subscribe(() => {
        user.roles = [...rolesToUpdate.roleNames];
      }, err => this.as.error(err));
    }
  });
  }

  getRolesArray(user) {
    const userRoles = user.roles;
    const rolesForModal: any[] = [
      { name: 'Admin', checked: false },
      { name: 'Moderator', checked: false },
      { name: 'Member', checked: false }
    ];

    for (let i = 0; i < rolesForModal.length; i++) {
      for (let j = 0; j < userRoles.length; j++) {
        if (rolesForModal[i].name === userRoles[j]) {
          rolesForModal[i].checked = true;
          break;
        }
      }
    }

    return rolesForModal;
  }

}
