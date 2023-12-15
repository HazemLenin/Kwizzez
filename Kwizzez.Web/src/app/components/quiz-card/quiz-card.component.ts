import { Component, Input } from '@angular/core';
import Quiz from 'src/app/models/Quiz';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-quiz-card',
  templateUrl: './quiz-card.component.html',
  styleUrls: ['./quiz-card.component.css'],
})
export class QuizCardComponent {
  @Input() quiz: Quiz;
  constructor(private authService: AuthService) {}
  isTeacher$ = this.authService.hasRole('Teacher');
}
