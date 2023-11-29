import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import AddQuiz from '../models/AddQuiz';
import EditQuiz from '../models/EditQuiz';

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

  getQuizById(id: string): Observable<any> {
    return this.http.get(`${this.BASE_URL}/Quizzes/${id}`, {
      headers: this.HEADERS,
    });
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

  addQuiz(quiz: AddQuiz): Observable<any> {
    return this.http.post(`${this.BASE_URL}/Quizzes`, quiz, {
      headers: this.HEADERS,
    });
  }

  editQuiz(quiz: EditQuiz): Observable<any> {
    return this.http.put(`${this.BASE_URL}/Quizzes/${quiz.id}`, quiz, {
      headers: this.HEADERS,
    });
  }
}
