import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient) {}
  BASE_URL = 'https://localhost:7143/api';

  login(email: string, password: string): Observable<any> {
    return this.http.post(`${this.BASE_URL}/Auth/Login`, { email, password });
  }
}
