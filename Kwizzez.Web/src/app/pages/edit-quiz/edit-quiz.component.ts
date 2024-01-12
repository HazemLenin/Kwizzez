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
import {
  faCircleNotch,
  faPlus,
  faXmark,
  faTrash,
  faLock,
  faGlobe,
  faLink,
} from '@fortawesome/free-solid-svg-icons';
import { catchError, throwError } from 'rxjs';
import AnswerForm from 'src/app/models/AddAnswer';
import QuestionForm from 'src/app/models/AddQuestion';
import Quiz from 'src/app/models/Quiz';
import EditQuiz from 'src/app/models/EditQuiz';
import { QuizzesService } from 'src/app/services/quizzes.service';
import EditQuestion from 'src/app/models/EditQuestion';
import EditAnswer from 'src/app/models/EditAnswer';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-edit-quiz',
  templateUrl: './edit-quiz.component.html',
  styleUrls: ['./edit-quiz.component.css'],
})
export class EditQuizComponent implements OnInit {
  loadingQuiz = true;
  faXmark = faXmark;
  faPlus = faPlus;
  faLock = faLock;
  faGlobe = faGlobe;
  isPublic = false;
  code = '';
  link = '';

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private quizzesService: QuizzesService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService
  ) {}

  quizForm = this.formBuilder.group({
    title: ['', Validators.required],
    description: [''],
    questions: this.formBuilder.array([this.getNewQuestion()]),
  });

  faCircleNotch = faCircleNotch;
  faTrash = faTrash;
  faLink = faLink;
  loading: boolean = false;
  quizId: string;
  deleteModalShow = false;

  errors: string[] = [];

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.quizId = params.get('id') ?? '';
      this.quizzesService
        .getQuizDetails(params.get('id') ?? '')
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
            this.link = `${location.protocol}//${location.host}/quizzes/${response.data.id}`;
            this.isPublic = response.data.isPublic;
            this.code = response.data.code;
            this.quizForm.patchValue({
              title: response.data.title,
              description: response.data.description,
            });
            while (this.questions.length) {
              this.questions.removeAt(0);
            }

            response.data.questions.forEach((question: any) => {
              const questionFormGroup = this.formBuilder.group({
                id: new FormControl(null),
                title: new FormControl(''),
                degree: new FormControl(null),
                answers: this.formBuilder.array([]),
              });
              questionFormGroup.patchValue({
                id: question.id,
                title: question.title,
                degree: question.degree,
              });

              const answersFormArray = questionFormGroup.get(
                'answers'
              ) as FormArray;

              // Iterate over the answers in the backend data and add them to the answers form array
              question.answers.forEach((answer: any) => {
                const answerFormGroup = this.formBuilder.group({
                  id: new FormControl(null),
                  title: new FormControl(''),
                  isCorrect: new FormControl(false),
                });
                answerFormGroup.patchValue({
                  id: answer.id,
                  title: answer.title,
                  isCorrect: answer.isCorrect,
                });

                answersFormArray.push(answerFormGroup);
              });

              this.questions.push(questionFormGroup);
            });
          }

          this.loadingQuiz = false;
        });
    });
  }

  get title() {
    return this.quizForm.get('title');
  }

  get description() {
    return this.quizForm.get('description');
  }

  get questions() {
    return this.quizForm.get('questions') as FormArray;
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

  removeQuestion(index: number) {
    const questions = this.quizForm.get('questions') as FormArray;
    questions.removeAt(index);
  }

  addAnswer(questionIndex: number) {
    const question = this.questionGroups[questionIndex];
    const answers = question.get('answers') as FormArray;
    const newAnswer = this.getNewAnswer();
    answers.push(newAnswer);
  }

  removeAnswer(quetsionIndex: number, answerIndex: number) {
    const question = this.questionGroups[quetsionIndex];
    const answers = question.get('answers') as FormArray;
    answers.removeAt(answerIndex);
  }

  getNewQuestion() {
    return this.formBuilder.group({
      id: [''],
      title: ['', Validators.required],
      degree: [1, [Validators.required, Validators.min(1)]],
      answers: this.formBuilder.array([this.getNewAnswer()]),
    });
  }

  getNewAnswer() {
    return this.formBuilder.group({
      id: [''],
      title: ['', Validators.required],
      isCorrect: [false, Validators.required],
    });
  }

  copyLink() {
    navigator.clipboard.writeText(this.link);
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
        questions: this.questionGroups?.map((group, index): EditQuestion => {
          return {
            id: group.controls['id'].value || null,
            title: group.controls['title'].value,
            image: null,
            order: index + 1,
            degree: group.controls['degree'].value,
            answers: (
              (group.controls['answers'] as FormArray).controls as FormGroup[]
            ).map((answer, index): EditAnswer => {
              return {
                id: answer.controls['id'].value || null,
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
        this.toastr.success('Quiz edited successfully!');
        this.router.navigate(['/my-quizzes']);
      });
    }
  }

  openDeleteModal() {
    this.deleteModalShow = true;
  }
  closeDeleteModal() {
    this.deleteModalShow = false;
  }
  deleteQuiz() {
    this.quizzesService.deleteQuiz(this.quizId).subscribe((response) => {
      this.deleteModalShow = false;
      this.loading = false;
      this.toastr.success('Quiz deleted successfully!');
      this.router.navigate(['/my-quizzes']);
    });
  }
}
