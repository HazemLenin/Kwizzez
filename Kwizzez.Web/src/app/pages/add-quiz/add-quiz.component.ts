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
import {
  faCircleNotch,
  faPlus,
  faXmark,
} from '@fortawesome/free-solid-svg-icons';
import AddQuiz from 'src/app/models/AddQuiz';
import AnswerForm from 'src/app/models/AddAnswer';
import QuestionForm from 'src/app/models/AddQuestion';
import { QuizzesService } from 'src/app/services/quizzes.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-quiz',
  templateUrl: './add-quiz.component.html',
  styleUrls: ['./add-quiz.component.css'],
})
export class AddQuizComponent {
  constructor(
    private formBuilder: FormBuilder,
    private quizzesService: QuizzesService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  redirectUrl: String = '';
  faCircleNotch = faCircleNotch;
  faPlus = faPlus;
  faXmark = faXmark;
  loading: boolean = false;

  ngOnInit(): void {}

  quizForm = this.formBuilder.group({
    title: ['', Validators.required],
    description: [''],
    isPublic: [true, Validators.required],
    questions: this.formBuilder.array([this.getNewQuestion()]),
  });

  errors: string[] = [];

  get title() {
    return this.quizForm.get('title');
  }

  get description() {
    return this.quizForm.get('description');
  }

  get isPublic() {
    return this.quizForm.get('isPublic');
  }

  get questionGroups() {
    return (this.quizForm.get('questions') as FormArray)
      .controls as FormGroup[];
  }

  getIsCorrectControl(answerGroup: FormGroup): FormControl {
    return answerGroup.get('isCorrect') as FormControl;
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

    this.quizForm.markAllAsTouched();

    if (this.quizForm.valid) {
      this.loading = true;
      let data: AddQuiz = {
        title: this.title?.value || '',
        description: this.description?.value || null,
        isPublic: this.isPublic?.value ?? true,
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
        if (response.isSucceed) {
          this.toastr.success('Quiz added successfully!');
          this.router.navigate(['/edit-quiz', response.data]);
        } else {
          this.errors = Object.values(response.errors);
        }
      });
    }
  }
}
