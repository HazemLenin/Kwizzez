import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import RegistrationModel from '../models/RegistrationModel';
import { Store } from '@ngrx/store';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private http: HttpClient,
    private store: Store<{ isAuthenticated: boolean }>
  ) {}
  BASE_URL = 'http://localhost:5221/api';
  HEADERS = { Authorization: `Bearer ${this.getAccessToken()}` };

  login(email: string, password: string): Observable<any> {
    return this.http.post(
      `${this.BASE_URL}/Auth/Login`,
      { email, password },
      { headers: this.HEADERS }
    );
  }

  signup(registrationModel: RegistrationModel): Observable<any> {
    return this.http.post(`${this.BASE_URL}/Auth/Signup`, registrationModel, {
      headers: this.HEADERS,
    });
  }

  refreshToken() {
    return this.http.post(
      `${this.BASE_URL}/Auth/Signup`,
      {
        refreshToken: this.getRefreshToken(),
      },
      { headers: this.HEADERS }
    );
  }

  getAccessToken(): string {
    return localStorage.getItem('token') as string;
  }

  setAccessToken(token: string) {
    localStorage.setItem('token', token);
  }

  private getRefreshToken(): string {
    return localStorage.getItem('refreshToken') as string;
  }

  setRefreshToken(token: string) {
    localStorage.setItem('refrsehToken', token);
  }

  isAuthenticated(): Observable<boolean> {
    return this.store.select('isAuthenticated');
  }
}
