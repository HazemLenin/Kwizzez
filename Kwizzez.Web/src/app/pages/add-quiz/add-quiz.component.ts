import { Component, KeyValueDiffers } from '@angular/core';
import { AbstractControl, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { faCircleNotch } from '@fortawesome/free-solid-svg-icons';
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

  /*
  constructor(private fb: FormBuilder) {
    this.quizForm = this.fb.group({
      quizTitle: ['', Validators.required],
      questions: this.fb.array([]),
    });
  }

  get questionGroups() {
    return (this.quizForm.get('questions') as FormArray).controls as FormGroup[];
  }

  getAnswers(questionGroup: FormGroup) {
    return (questionGroup.get('answers') as FormArray).controls as FormGroup[];
  }

  addQuestion() {
    const questions = this.quizForm.get('questions') as FormArray;
    const newQuestion = this.fb.group({
      questionText: ['', Validators.required],
      answers: this.fb.array([]),
    });
    questions.push(newQuestion);
  }

  addAnswer(questionIndex: number) {
    const question = this.questionGroups[questionIndex];
    const answers = question.get('answers') as FormArray;
    const newAnswer = this.fb.group({
      answerText: ['', Validators.required],
    });
    answers.push(newAnswer);
  }
  */

  quizForm = this.formBuilder.group({
    title: ['', Validators.required],
    hasLimitedTime: [false, Validators.required],
    hours: [0, [this.requiredIfhasLimitedTimeValidator, Validators.min(0)]],
    minutes: [
      0,
      [
        this.requiredIfhasLimitedTimeValidator,
        Validators.min(0),
        Validators.max(59),
      ],
    ],
    publishDate: [null, Validators.required],
    expirationDate: [null, [Validators.required, this.greaterThanPublishDate]],
    isPublic: [false, Validators.required],
    // questions: [, Validators.required],
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

  greaterThanPublishDate(
    control: AbstractControl
  ): { [key: string]: any } | null {
    const publishDate = control.parent?.get('publishDate')?.value;

    // If the checkbox is checked and the text input is empty, mark it as required
    if (publishDate && !control.value) {
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

  addQuiz() {
    this.errors = [];

    if (!this.hasLimitedTime?.value) {
      this.hours?.setValue(0);
      this.minutes?.setValue(0);
    }

    this.quizForm.markAllAsTouched();

    if (this.quizForm.valid) {
      console.log();
      this.loading = true;
      this.title?.disable();
      this.hasLimitedTime?.disable();
      this.hours?.disable();
      this.minutes?.disable();
      this.publishDate?.disable();
      this.expirationDate?.disable();
      this.isPublic?.disable();
      console.log({
        title: this.title?.value || '',
        hasLimitedTime: this.hasLimitedTime?.value || false,
        publishDate: this.publishDate?.value || null,
        expirationDate: this.expirationDate?.value || null,
        isPublic: this.isPublic?.value || false,
        timeLimitTicks:
          (this.hours?.value ?? 0) * 60 * 60 * 1000 +
          (this.minutes?.value ?? 0) * 60 * 1000,
      });
      // this.quizzesService
      //   .addQuiz({
      //     title: this.title?.value || '',
      //     hasLimitedTime: this.hasLimitedTime?.value || false,
      //     publishDate: this.publishDate?.value || null,
      //     expirationDate: this.expirationDate?.value || null,
      //     isPublic: this.isPublic?.value || false,
      //     timeLimitTicks:
      //       (this.hours?.value ?? 0) * 60 * 60 * 1000 +
      //       (this.minutes?.value ?? 0) * 60 * 1000,
      //   })
      //   .subscribe((response) => {
      //     this.loading = false;
      //     this.title?.enable();
      //     this.hasLimitedTime?.enable();
      //     this.hours?.enable();
      //     this.minutes?.enable();
      //     this.publishDate?.enable();
      //     this.expirationDate?.enable();
      //     this.isPublic?.enable();

      //     if (response.isSucceed) {
      //       // store it in the database
      //       this.router.navigate(['quiz', response.data]);
      //     } else {
      //       this.errors = Object.values(response.errors);
      //     }
      //   });
    }
  }
}
