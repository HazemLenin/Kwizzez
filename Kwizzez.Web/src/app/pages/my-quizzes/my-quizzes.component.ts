import { Component, OnInit } from '@angular/core';
import Quiz from 'src/app/models/Quiz';
import { QuizzesService } from 'src/app/services/quizzes.service';

@Component({
  selector: 'app-my-quizzes',
  templateUrl: './my-quizzes.component.html',
  styleUrls: ['./my-quizzes.component.css'],
})
export class MyQuizzesComponent implements OnInit {
  constructor(private quizzesService: QuizzesService) {}
  quizzes: Quiz[];
  ngOnInit(): void {
    this.quizzesService.getCurrentUsersQuizzes().subscribe((response) => {
      if (response.isSucceed) {
        this.quizzes = response.data;
      }
    });
  }
}
