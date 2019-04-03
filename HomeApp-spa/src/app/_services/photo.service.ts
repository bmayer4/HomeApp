import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class PhotoService {

baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

setCoverPhoto(homeId: number, id: number) {
  return this.http.patch(this.baseUrl + 'homes/' + homeId + '/photos/' + id + '/setCover', {});
}

deletePhoto(homeId: number, id: number) {
  return this.http.delete(this.baseUrl + 'homes/' + homeId + '/photos/' + id);
}

}
