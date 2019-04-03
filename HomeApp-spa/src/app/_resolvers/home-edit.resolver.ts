import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { HomeService } from '../_services/home.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';


@Injectable({providedIn: 'root' })
export class HomeEditResolver implements Resolve<any> {

    constructor(private homeService: HomeService, private router: Router) {}

    resolve(route: ActivatedRouteSnapshot): Observable<any> {
        return this.homeService.getHome(route.params['id']).pipe(catchError(error => {
            console.log(error);
            this.router.navigate(['/']);
            return of(null);
        }));
    }
}
