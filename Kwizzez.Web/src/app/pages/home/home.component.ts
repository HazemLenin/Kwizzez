import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

import { ToastrService } from 'ngx-toastr';
import { QuizzesService } from 'src/app/services/quizzes.service';
import Quiz from '../../models/Quiz';
import { AuthService } from 'src/app/services/auth.service';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/User';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  quizzes: Quiz[];
  loading: Boolean = true;
  user$: Observable<User>;
  isAuthenticated$: Observable<boolean>;

  constructor(
    private quizzesService: QuizzesService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.isAuthenticated$ = this.authService.isAuthenticated();
    this.isAuthenticated$.subscribe((isAuthenticated) => {
      if (isAuthenticated) {
        this.user$ = this.authService.getUser();
      }
    });

    this.quizzesService.getQuizzes(1).subscribe((response) => {
      this.loading = false;
      if (response.isSucceed) {
        this.quizzes = response.data;
      }
    });
  }
}
