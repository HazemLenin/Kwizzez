import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import RegistrationModel from '../models/RegistrationModel';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient) {}
  BASE_URL = 'http://localhost:5221/api';

  login(email: string, password: string): Observable<any> {
    return this.http.post(`${this.BASE_URL}/Auth/Login`, { email, password });
  }

  signup(registrationModel: RegistrationModel): Observable<any> {
    return this.http.post(`${this.BASE_URL}/Auth/Signup`, registrationModel);
  }
}
