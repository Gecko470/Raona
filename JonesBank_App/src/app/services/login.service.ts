import { Injectable } from '@angular/core';
import { UserLogin, respLogin } from '../interfaces/interfaces';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { environment } from '../../../enviroments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http: HttpClient) { }

    login(user: UserLogin): Observable<respLogin> {
      return this.http.post<respLogin>(`${environment.baseUrlLogin}/login`, user);
    }
}
