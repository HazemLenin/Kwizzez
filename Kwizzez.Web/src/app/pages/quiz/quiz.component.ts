import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { faCircleNotch } from '@fortawesome/free-solid-svg-icons';
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
  selectedAnswers = new Map<string, string>(); // Map<questionId, answerId>
  faCircleNotch = faCircleNotch;

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
    this.quizzesService.startQuiz(this.quiz.id).subscribe(() => {
      this.loadQuestions();
    });
  }

  resumeQuiz() {
    this.quizzesService.getAnswers(this.quiz.id).subscribe((response) => {
      this.selectedAnswers = new Map<string, string>(
        Object.entries(response.data.answersIds)
      );
      if (response.isSucceed) this.loadQuestions();
    });
    this.loadQuestions();
  }

  getResults() {
    this.quizzesService.getAnswers(this.quiz.id).subscribe((response) => {
      if (response.isSucceed) this.loadQuestions();
    });
  }

  private loadQuestions() {
    this.loading = true;
    this.quizzesService.getQuizQuestions(this.quiz.id).subscribe((response) => {
      this.loading = false;
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

  selectAnswer(questionId: string, answerId: string) {
    if (!this.answerSelected(questionId, answerId))
      this.quizzesService.selectAnswer(this.quiz.id, answerId).subscribe(() => {
        console.log('setting up');
        this.selectedAnswers.set(questionId, answerId);
      });
  }

  answerSelected(questionId: string, answerId: string) {
    let selectedAnswer = this.selectedAnswers.get(questionId);
    if (selectedAnswer) return selectedAnswer == answerId;
    return false;
  }
}
