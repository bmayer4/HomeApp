import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class HomeService {

baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

getHomes(): Observable<any> {
    return this.http.get(this.baseUrl + 'homes');
}

getHome(id: number): Observable<any> {
    return this.http.get(this.baseUrl + 'homes/' + id);
}

}
