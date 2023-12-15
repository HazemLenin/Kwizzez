import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import Question from 'src/app/models/Question';
import Quiz from 'src/app/models/Quiz';
import { QuizzesService } from 'src/app/services/quizzes.service';

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.css'],
})
export class QuizComponent implements OnInit {
  loading = true;
  quiz: Quiz;
  quizStarted = false;
  questions: Question[];
  selectedAnswers = new Map<String, String>(); // Map<questionId, answerId>

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
          this.loading = false;
          if (response.isSucceed) {
            this.quiz = response.data;
          }
        });
    });
  }

  takeQuiz() {
    this.quizzesService.getQuizQuestions(this.quiz.id).subscribe((response) => {
      if (response.isSucceed) {
        this.quizStarted = true;
        this.questions = response.data;

        this.questions = this.questions.sort(
          (a, b) => (a.order as number) - (b.order as number)
        );

        this.questions = this.questions.map((question) => {
          question.answers = question.answers.sort(
            (a, b) => (a.order as number) - (b.order as number)
          );
          return question;
        });
      }
    });
  }

  selectAnswer(questionId: String, answerId: String) {
    this.selectedAnswers.set(questionId, answerId);
  }

  answerSelected(questionId: String, answerId: String) {
    let selectedAnswer = this.selectedAnswers.get(questionId);
    if (selectedAnswer) return selectedAnswer == answerId;
    return false;
  }
}
