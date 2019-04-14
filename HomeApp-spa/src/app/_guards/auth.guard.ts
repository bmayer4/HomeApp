import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot): boolean {

    // for admin route
    const requiredRoles = route.data['roles'] as string[];
    if (requiredRoles) {
      if (this.authService.roleMatch(requiredRoles)) {
        return true;
      } else {
        this.router.navigate(['/']);
        return false;
      }
    }

    if (this.authService.isAuth()) {
      return true;
    }

    this.router.navigate(['/']);
    return false;
  }

}
