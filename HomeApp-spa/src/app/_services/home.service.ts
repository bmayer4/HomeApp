import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Home } from '../_models/home';

@Injectable({
    providedIn: 'root'
})
export class HomeService {

baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

getHomes(): Observable<Home[]> {
    return this.http.get<Home[]>(this.baseUrl + 'homes');
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
