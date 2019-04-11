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
  serverError: any;

  constructor(private authService: AuthService, private router: Router, private as: AlertifyService) { }

  ngOnInit() {
  }

  login(loginForm: NgForm) {
    this.serverError = null;
    this.authService.login(loginForm.value).subscribe(result => {
      this.as.success('Logged in');
      this.serverError = null;
      this.router.navigate(['/homes']);
    }, err => {
      // tslint:disable-next-line:triple-equals
      if (err.trim() == 'The Password field is required.') {
        loginForm.controls.password.setErrors({ required: true });
      } else {
        this.serverError = err;
      }
    });
  }

}
