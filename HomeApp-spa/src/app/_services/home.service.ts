import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Home } from '../_models/home';
import { map } from 'rxjs/operators';
import { PaginatedResult } from '../_models/pagination';

@Injectable({
    providedIn: 'root'
})
export class HomeService {

baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

getHomes(currentPage?, pageSize?): Observable<PaginatedResult<Home[]>> {
    const paginatedResult: PaginatedResult<Home[]> = new PaginatedResult<Home[]>();
    let params = new HttpParams();
    if (currentPage != null && pageSize != null) {
        params = params.append('currentPage', currentPage);
        params = params.append('pageSize', pageSize);
    }
    console.log(params);
    return this.http.get<Home[]>(this.baseUrl + 'homes', { observe: 'response', params: params }).pipe(
        map(response => {
            paginatedResult.result = response.body;
              if (response.headers.get('Pagination') != null) {
                paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
              }
              return paginatedResult;
        })
    );
}

getHomesByUser(): Observable<Home[]> {
    return this.http.get<Home[]>(this.baseUrl + 'homes/myHomes');
}

getHome(id: number): Observable<Home> {
    return this.http.get<Home>(this.baseUrl + 'homes/' + id);
}

addHome(model: any): Observable<Home> {
    return this.http.post<Home>(this.baseUrl + 'homes/', model);
}

updateHome(home: Home) {
    return this.http.patch(this.baseUrl + 'homes/' + home.id, home);
}

deleteHome(id: number) {
    return this.http.delete(this.baseUrl + 'homes/' + id);
}

}
