import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map, switchMap } from 'rxjs';
import RegistrationModel from '../models/RegistrationModel';
import { Store } from '@ngrx/store';
import { User } from '../models/User';
import { loadUser } from '../states/user/user.actions';
import Tokens from '../models/Tokens';
import { Role } from '../types/Role';
// import Roles from '../types/Role';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private http: HttpClient,
    private store: Store<{ tokens: Tokens; user: User }>
  ) {
    this.getTokens().subscribe((tokens) => {
      if (tokens != null) {
        this.refreshToken = tokens?.refreshToken;
        this.HEADERS = { Authorization: `Bearer ${tokens?.token}` };
      }
    });
  }

  private refreshToken: string;
  HEADERS = {};
  BASE_URL = 'http://localhost:5221/api';

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

  refreshTokens() {
    return this.http.post(
      `${this.BASE_URL}/Auth/Signup`,
      {
        refreshToken: this.refreshToken,
      },
      { headers: this.HEADERS }
    );
  }

  getTokens(): Observable<Tokens> {
    return this.store.select('tokens');
  }

  getUser(): Observable<User> {
    return this.store.select('user');
  }

  UpdateUser(): void {
    this.http
      .get(`${this.BASE_URL}/Auth/Profile`, { headers: this.HEADERS })
      .subscribe((response: any) => {
        this.store.dispatch(loadUser({ payload: response.data }));
      });
  }

  isAuthenticated(): Observable<boolean> {
    return this.getTokens().pipe(
      map((tokens) => {
        return tokens == null ? false : true;
      })
    );
  }

  private getUserRoles(): Observable<any> {
    return this.http.get(`${this.BASE_URL}/Auth/Roles`, {
      headers: this.HEADERS,
    });
  }

  HasRole(role: Role): Observable<boolean> {
    return this.getUserRoles().pipe(
      map(({ isSucceed, data }: { isSucceed: Boolean; data: Role[] }) => {
        if (!isSucceed) return false;
        if (!data.includes('Teacher')) return false;
        return true;
      })
    );
  }
}
