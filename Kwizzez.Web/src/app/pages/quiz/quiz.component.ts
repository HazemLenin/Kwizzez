import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { faCircleNotch } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
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
  showingResults = false;
  score = 0;
  questions: Question[];
  selectedAnswers = new Map<string, string>(); // Map<questionId, answerId>
  faCircleNotch = faCircleNotch;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private quizzesService: QuizzesService,
    private toastr: ToastrService
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
    this.loading = true;
    let code = this.quiz.isPublic ? '0' : prompt('enter code');
    this.quizzesService
      .startQuiz(this.quiz.id, parseInt(code || '0') || 0)
      .pipe(
        catchError((err) => {
          if (err.status == 403) this.toastr.error('Invalid code');
          else this.toastr.error('Something went wrong');
          this.loading = false;
          return throwError(() => err);
        })
      )
      .subscribe(() => {
        this.loadQuestions(false);
        this.loading = false;
      });
  }

  resumeQuiz() {
    this.loading = true;
    this.quizzesService.getAnswers(this.quiz.id).subscribe((response) => {
      this.selectedAnswers = new Map<string, string>(
        Object.entries(response.data.answersIds)
      );
      if (response.isSucceed) this.loadQuestions(false);
      this.loading = false;
    });
  }

  getResults() {
    this.loading = true;
    this.quizzesService.getAnswers(this.quiz.id).subscribe((response) => {
      this.selectedAnswers = new Map<string, string>(
        Object.entries(response.data.answersIds)
      );
      this.score = response.data.score;
      if (response.isSucceed) this.loadQuestions(true);
      this.loading = false;
    });
  }

  private loadQuestions(forResults: boolean) {
    this.quizzesService.getQuizQuestions(this.quiz.id).subscribe((response) => {
      if (response.isSucceed) {
        if (forResults) this.showingResults = true;
        else this.quizStarted = true;

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

  selectAnswer(questionId: string, answerId: string) {
    if (!this.answerSelected(questionId, answerId))
      this.quizzesService.selectAnswer(this.quiz.id, answerId).subscribe(() => {
        this.selectedAnswers.set(questionId, answerId);
      });
  }

  answerSelected(questionId: string, answerId: string) {
    let selectedAnswer = this.selectedAnswers.get(questionId);
    if (selectedAnswer) return selectedAnswer == answerId;
    return false;
  }

  submit() {
    this.quizzesService.submitQuiz(this.quiz.id).subscribe(() => {
      location.reload();
    });
  }
}
