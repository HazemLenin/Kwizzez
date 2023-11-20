import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import QuizForm from '../models/QuizForm';

@Injectable({
  providedIn: 'root',
})
export class QuizzesService {
  constructor(private http: HttpClient, private authService: AuthService) {
    this.authService.getTokens().subscribe((tokens) => {
      this.HEADERS = { Authorization: `Bearer ${tokens?.token}` };
    });
  }

  HEADERS = {};
  BASE_URL = 'http://localhost:5221/api';

  getQuizzes(pageNumber: number): Observable<any> {
    return this.http.get(`${this.BASE_URL}/Quizzes/`);
  }

  getQuizInfoById(id: string): Observable<any> {
    return this.http.get(`${this.BASE_URL}/Quizzes/GetQuizInfo/${id}`, {
      headers: this.HEADERS,
    });
  }

  getCurrentUsersQuizzes(): Observable<any> {
    return this.http.get(`${this.BASE_URL}/Quizzes/MyQuizzes`, {
      headers: this.HEADERS,
    });
  }

  addQuiz(quiz: QuizForm): Observable<any> {
    return this.http.post(`${this.BASE_URL}/Quizzes`, quiz, {
      headers: this.HEADERS,
    });
  }
}
