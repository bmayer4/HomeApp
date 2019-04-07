import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { HomeService } from '../_services/home.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';


@Injectable({providedIn: 'root' })
export class FavHomesResolver implements Resolve<any> {

    currentPage = 1;
    pageSize = 3;

    constructor(private homeService: HomeService, private router: Router) {}

    resolve(route: ActivatedRouteSnapshot): Observable<any> {
        return this.homeService.getFavHomesByUser(this.currentPage, this.pageSize).pipe(catchError(error => {
            console.log(error);
            this.router.navigate(['/']);
            return of(null);
        }));
    }
}
