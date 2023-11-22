import { Component, KeyValueDiffers } from '@angular/core';
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
import AnswerForm from 'src/app/models/AnswerForm';
import QuestionForm from 'src/app/models/QuestionForm';
import QuizForm from 'src/app/models/QuizForm';
import { QuizzesService } from 'src/app/services/quizzes.service';

@Component({
  selector: 'app-add-quiz',
  templateUrl: './add-quiz.component.html',
  styleUrls: ['./add-quiz.component.css'],
})
export class AddQuizComponent {
  constructor(
    private formBuilder: FormBuilder,
    private quizzesService: QuizzesService,
    private router: Router
  ) {}

  redirectUrl: String = '';
  faCircleNotch = faCircleNotch;
  loading: boolean = false;

  ngOnInit(): void {}

  quizForm = this.formBuilder.group({
    title: ['', Validators.required],
    hasLimitedTime: [false, Validators.required],
    hours: [
      0,
      [
        this.requiredIfhasLimitedTimeValidator,
        this.requiredIfMinutesIsZero,
        Validators.min(0),
      ],
    ],
    minutes: [
      0,
      [
        this.requiredIfhasLimitedTimeValidator,
        Validators.min(0),
        Validators.max(59),
      ],
    ],
    publishDate: [null, Validators.required],
    expirationDate: [
      null,
      [Validators.required, this.greaterThanPublishDate, this.greaterThanNow],
    ],
    isPublic: [false, Validators.required],
    questions: this.formBuilder.array([this.getNewQuestion()]),
  });

  requiredIfhasLimitedTimeValidator(
    control: AbstractControl
  ): { [key: string]: any } | null {
    const hasLimitedTime = control.parent?.get('hasLimitedTime')?.value;

    // If the checkbox is checked and the text input is empty, mark it as required
    if (hasLimitedTime && !control.value) {
      return { requiredIfHasLimitedTime: true };
    }

    return null;
  }

  requiredIfMinutesIsZero(
    control: AbstractControl
  ): { [key: string]: any } | null {
    const minutes = control.parent?.get('minutes')?.value;
    const hasLimitedTime = control.parent?.get('hasLimitedTime')?.value;

    if (!minutes && !control.value && hasLimitedTime) {
      return { required: true };
    }

    return null;
  }

  greaterThanNow(control: AbstractControl): { [key: string]: any } | null {
    if (
      new Date(control.value) <
      new Date(
        new Date().getFullYear(),
        new Date().getMonth(),
        new Date().getDate()
      )
    ) {
      console.log('not valid');
      return { lessThanNow: true };
    }

    return null;
  }

  greaterThanPublishDate(
    control: AbstractControl
  ): { [key: string]: any } | null {
    const publishDate = control.parent?.get('publishDate')?.value;

    // If the checkbox is checked and the text input is empty, mark it as required
    if (new Date(publishDate) > new Date(control.value)) {
      return { greaterThanPublishDate: true };
    }

    return null;
  }

  errors: string[] = [];

  get title() {
    return this.quizForm.get('title');
  }

  get hasLimitedTime() {
    return this.quizForm.get('hasLimitedTime');
  }

  get hours() {
    return this.quizForm.get('hours');
  }

  get minutes() {
    return this.quizForm.get('minutes');
  }

  get publishDate() {
    return this.quizForm.get('publishDate');
  }

  get expirationDate() {
    return this.quizForm.get('expirationDate');
  }

  get isPublic() {
    return this.quizForm.get('isPublic');
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

  addQuiz() {
    this.errors = [];

    if (!this.hasLimitedTime?.value) {
      this.hours?.setValue(0);
      this.minutes?.setValue(0);
    }

    this.quizForm.markAllAsTouched();

    if (this.quizForm.valid) {
      this.loading = true;
      // this.title?.disable();
      // this.hasLimitedTime?.disable();
      // this.hours?.disable();
      // this.minutes?.disable();
      // this.publishDate?.disable();
      // this.expirationDate?.disable();
      // this.isPublic?.disable();
      let data: QuizForm = {
        title: this.title?.value || '',
        hasLimitedTime: this.hasLimitedTime?.value || false,
        publishDate: this.publishDate?.value || null,
        expirationDate: this.expirationDate?.value || null,
        isPublic: this.isPublic?.value || false,
        timeLimitTicks:
          (this.hours?.value ?? 0) * 60 * 60 * 1000 +
          (this.minutes?.value ?? 0) * 60 * 1000,
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
      this.quizzesService.addQuiz(data).subscribe((response) => {
        this.loading = false;
        // this.title?.enable();
        // this.hasLimitedTime?.enable();
        // this.hours?.enable();
        // this.minutes?.enable();
        // this.publishDate?.enable();
        // this.expirationDate?.enable();
        // this.isPublic?.enable();

        if (response.isSucceed) {
          this.router.navigate(['quiz', response.data.id]);
        } else {
          this.errors = Object.values(response.errors);
        }
      });
    }
  }
}
