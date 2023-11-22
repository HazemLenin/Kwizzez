import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import Quiz from 'src/app/models/Quiz';
import { QuizzesService } from 'src/app/services/quizzes.service';

@Component({
  selector: 'app-edit-quiz',
  templateUrl: './edit-quiz.component.html',
  styleUrls: ['./edit-quiz.component.css'],
})
export class EditQuizComponent {
  loading = false;
  quiz: Quiz;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private quizzesService: QuizzesService
  ) {}
  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.quizzesService
        .getQuizById(params.get('id') ?? '')
        .pipe(
          catchError((err) => {
            if (err.status === 404) {
              return this.router.navigate(['/notFound'], {
                skipLocationChange: true,
              });
            }
            return throwError(() => err);
          })
        )
        .subscribe((response) => {
          this.loading = true;
          if (response.isSucceed) {
            this.quiz = response.data;
            console.log(this.quiz);
          }
        });
    });
  }
}