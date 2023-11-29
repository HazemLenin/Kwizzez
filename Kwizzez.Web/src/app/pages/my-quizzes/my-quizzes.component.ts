import { Component, OnInit } from '@angular/core';
import Quiz from 'src/app/models/Quiz';
import { QuizzesService } from 'src/app/services/quizzes.service';
import { faPlus } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-my-quizzes',
  templateUrl: './my-quizzes.component.html',
  styleUrls: ['./my-quizzes.component.css'],
})
export class MyQuizzesComponent implements OnInit {
  constructor(private quizzesService: QuizzesService) {}
  quizzes: Quiz[];
  loading = true;
  faPlus = faPlus;

  ngOnInit(): void {
    this.quizzesService.getCurrentUsersQuizzes().subscribe((response) => {
      this.loading = false;
      if (response.isSucceed) {
        this.quizzes = response.data;
      }
    });
  }
}
