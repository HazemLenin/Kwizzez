import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

import { ToastrService } from 'ngx-toastr';
import { QuizzesService } from 'src/app/services/quizzes.service';
import Quiz from '../../models/Quiz';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  quizzes: Quiz[];
  loading: Boolean = true;
  constructor(private quizzesService: QuizzesService) {}
  ngOnInit() {
    this.quizzesService.getQuizzes(1).subscribe((response) => {
      this.loading = false;
      if (response.isSuccess) {
        this.quizzes = response.data;
      }
    });
  }
}
