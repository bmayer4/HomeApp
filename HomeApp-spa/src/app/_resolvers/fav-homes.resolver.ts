import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { HomeService } from '../_services/home.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AlertifyService } from '../_services/alertify.service';


@Injectable({providedIn: 'root' })
export class FavHomesResolver implements Resolve<any> {

    currentPage = 1;
    pageSize = 3;

    constructor(private homeService: HomeService, private router: Router, private as: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<any> {
        return this.homeService.getFavHomesByUser(this.currentPage, this.pageSize).pipe(catchError(error => {
            this.as.error('Error retrieving data');
            this.router.navigate(['/']);
            return of(null);
        }));
    }
}
