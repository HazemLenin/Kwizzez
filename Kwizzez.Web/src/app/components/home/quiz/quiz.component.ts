import { Component, Input } from '@angular/core';
import Quiz from 'src/app/models/Quiz';

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.css'],
})
export class QuizComponent {
  @Input() quiz: Quiz;
}
