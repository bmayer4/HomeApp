import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { HomeService } from '../_services/home.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AlertifyService } from '../_services/alertify.service';


@Injectable({providedIn: 'root' })
export class HomeDetailResolver implements Resolve<any> {

    constructor(private homeService: HomeService, private router: Router, private as: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<any> {
        return this.homeService.getHome(route.params['id']).pipe(catchError(error => {
            this.as.error('Error retrieving data');   // error is from interceptor
            this.router.navigate(['/']);
            return of(null);
        }));
    }
}
