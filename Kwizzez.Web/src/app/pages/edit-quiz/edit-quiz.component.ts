import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { faCircleNotch } from '@fortawesome/free-solid-svg-icons';
import { catchError, throwError } from 'rxjs';
import AnswerForm from 'src/app/models/AnswerForm';
import QuestionForm from 'src/app/models/QuestionForm';
import Quiz from 'src/app/models/Quiz';
import EditQuiz from 'src/app/models/EditQuiz';
import { QuizzesService } from 'src/app/services/quizzes.service';

@Component({
  selector: 'app-edit-quiz',
  templateUrl: './edit-quiz.component.html',
  styleUrls: ['./edit-quiz.component.css'],
})
export class EditQuizComponent implements OnInit {
  loadingQuiz = true;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private quizzesService: QuizzesService,
    private formBuilder: FormBuilder
  ) {}

  quizForm = this.formBuilder.group({
    title: ['', Validators.required],
    description: [''],
    questions: this.formBuilder.array([this.getNewQuestion()]),
  });

  faCircleNotch = faCircleNotch;
  loading: boolean = false;
  quizId: String;

  errors: string[] = [];

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.quizId = params.get('id') ?? '';
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
          if (response.isSucceed) {
            this.quizForm.controls.description.setValue(
              response.data.description
            ),
              this.quizForm.controls.title.setValue(response.data.title),
              this.quizForm.controls.questions.patchValue(
                this.setQuestions(response.data.questions)
              );
          }

          this.loadingQuiz = false;
        });
    });
  }

  setQuestions(questions: any) {
    return questions
      .sort((a: any, b: any) => a.order - b.order)
      .map((question: any) => ({
        tilte: question.title,
        degree: question.degree,
        answers: question.answers
          .sort((a: any, b: any) => a.order - b.order)
          .map((answer: any) => ({
            title: answer.title,
            isCorrect: answer.isCorrect,
          })),
      }));
  }
  get title() {
    return this.quizForm.get('title');
  }

  get description() {
    return this.quizForm.get('description');
  }

  get questionGroups() {
    return (this.quizForm.get('questions') as FormArray)
      .controls as FormGroup[];
  }

  getAnswers(questionGroup: FormGroup) {
    return (questionGroup.get('answers') as FormArray).controls as FormGroup[];
  }

  addQuestion() {
    const questions = this.quizForm.get('questions') as FormArray;
    const newQuestion = this.getNewQuestion();
    questions.push(newQuestion);
  }

  addAnswer(questionIndex: number) {
    const question = this.questionGroups[questionIndex];
    const answers = question.get('answers') as FormArray;
    const newAnswer = this.getNewAnswer();
    answers.push(newAnswer);
  }

  getNewQuestion() {
    return this.formBuilder.group({
      title: ['', Validators.required],
      degree: [1, [Validators.required, Validators.min(1)]],
      answers: this.formBuilder.array([this.getNewAnswer()]),
    });
  }

  getNewAnswer() {
    return this.formBuilder.group({
      title: ['', Validators.required],
      isCorrect: [false, Validators.required],
    });
  }

  editQuiz() {
    this.errors = [];

    this.quizForm.markAllAsTouched();

    if (this.quizForm.valid) {
      this.loading = true;
      let data: EditQuiz = {
        id: this.quizId,
        title: this.title?.value || '',
        description: this.description?.value || null,
        questions: this.questionGroups?.map((group, index): QuestionForm => {
          return {
            title: group.controls['title'].value,
            image: null,
            order: index + 1,
            degree: group.controls['degree'].value,
            answers: (
              (group.controls['answers'] as FormArray).controls as FormGroup[]
            ).map((answer, index): AnswerForm => {
              return {
                title: answer.controls['title'].value,
                isCorrect: answer.controls['isCorrect'].value,
                order: index + 1,
              };
            }),
          };
        }),
      };
      this.quizzesService.editQuiz(data).subscribe((response) => {
        this.loading = false;

        if (response.isSucceed) {
          this.router.navigate(['/my-quizzes']);
        } else {
          this.errors = Object.values(response.errors);
        }
      });
    }
  }
}
