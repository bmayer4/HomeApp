import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model: any = {};

  constructor(private authService: AuthService, private router: Router, private as: AlertifyService) { }

  ngOnInit() {
  }

  login(loginForm: NgForm) {
    this.authService.login(loginForm.value).subscribe(result => {
      this.as.success('Logged in');
      this.router.navigate(['/homes']);
    }, err => console.log(err));
  }

}
