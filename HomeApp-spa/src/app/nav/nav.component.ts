import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  isCollapsed = true;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
  }

  isAuth() {
    return this.authService.isAuth();
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }

}
