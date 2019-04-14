import { Component, OnInit } from '@angular/core';
import { Home } from 'src/app/_models/home';
import { ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { HomeService } from 'src/app/_services/home.service';
import { AdminService } from 'src/app/_services/admin.service';

@Component({
  selector: 'app-home-management',
  templateUrl: './home-management.component.html',
  styleUrls: ['./home-management.component.css']
})
export class HomeManagementComponent implements OnInit {

  homes: Home[];
  pagination: Pagination;

  constructor(
    private route: ActivatedRoute,
    private homeService: HomeService,
    private adminService: AdminService,
    private as: AlertifyService
    ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.homes = data['homes'].result;
      this.pagination = data['homes'].pagination;
    });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.getHomes();
  }

  getHomes() {
    this.homeService.getHomes(this.pagination.currentPage, this.pagination.pageSize)
    .subscribe((res: PaginatedResult<Home[]>) => {
      this.homes = res.result;
      this.pagination =  res.pagination;
    }, err => this.as.error(err));
  }

  deleteHome(homeId: number) {
    this.as.confirm('Are you sure you want to delete this home?', () => {
      this.adminService.deleteHomeByModerator(homeId).subscribe(res => {
        this.as.success('Home deleted');
        this.homes = this.homes.filter(h => h.id !== homeId);
      }, err => this.as.error(err));
    });
   }

}
