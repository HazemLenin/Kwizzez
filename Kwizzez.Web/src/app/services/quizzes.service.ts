import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class QuizzesService {
  constructor(private http: HttpClient, private authService: AuthService) {}
  BASE_URL = 'http://localhost:5221/api';
  HEADERS = { Authorization: `Bearer ${this.authService.getAccessToken()}` };

  getQuizzes(pageNumber: number): Observable<any> {
    return this.http.get(`${this.BASE_URL}/Quizzes/`);
  }
}
