import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = environment.apiUrl;
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentUser: User;

  constructor(private http: HttpClient) { }

  register(model: any) {
    return this.http.post(this.baseUrl + 'auth/register', model);
  }

  login(model: any) {
      return this.http.post(this.baseUrl + 'auth/login', model).pipe(map((response: any) => {
        localStorage.setItem('token', response.token);
        localStorage.setItem('user', JSON.stringify(response.user));
        this.decodedToken = this.jwtHelper.decodeToken(response.token);
        this.currentUser = response.user;
      }));
  }

  isAuth() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.decodedToken = null;
    this.currentUser = null;
  }
}
