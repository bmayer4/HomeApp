import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
baseUrl = environment.apiUrl;

constructor(private http: HttpClient) {}

getUsersWithRoles(): Observable<any> {
  return this.http.get(this.baseUrl + 'admin/usersWithRoles');
}

updateUserRoles(userId: Number, roles: object) {
  return this.http.patch(this.baseUrl + 'admin/editRoles/' + userId, roles);
}

deleteHomeByModerator(id: number) {
  return this.http.delete(this.baseUrl + 'admin/homes/' + id);
}

}
