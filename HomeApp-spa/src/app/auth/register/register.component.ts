import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  bsConfig: Partial<BsDatepickerConfig> = { containerClass: 'theme-default' };

  constructor(private authService: AuthService, private fb: FormBuilder, private router: Router) { }

  ngOnInit() {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(14)]],
      dateOfBirth: [null, Validators.required],
      confirmPassword: ['', Validators.required]
    }, { validator: [this.validatePasswordMatch] });
  }

  validatePasswordMatch(g: FormGroup) {
    return g.get('password').value !== g.get('confirmPassword').value ? {'mismatch': true} : null;
  }

  register() {
      this.authService.register(this.registerForm.value).subscribe(result => {
      // TODO - add, get errors working and add for email in use
        this.authService.login(this.registerForm.value).subscribe(() => {
          this.router.navigate(['/homes']);
        }, err => console.log(err));
      }, err => console.log(err));
  }

}
