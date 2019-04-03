import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  isCollapsed = true;

  constructor(private authService: AuthService, private router: Router, private as: AlertifyService) { }

  ngOnInit() {
  }

  isAuth() {
    return this.authService.isAuth();
  }

  logout() {
    this.authService.logout();
    this.as.success('Logged out');
    this.router.navigate(['/']);
  }

}
