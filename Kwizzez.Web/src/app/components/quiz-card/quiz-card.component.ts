import { Component, Input } from '@angular/core';
import { faGlobe, faLock } from '@fortawesome/free-solid-svg-icons';
import Quiz from 'src/app/models/Quiz';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-quiz-card',
  templateUrl: './quiz-card.component.html',
  styleUrls: ['./quiz-card.component.css'],
})
export class QuizCardComponent {
  @Input() quiz: Quiz;
  faGlobe = faGlobe;
  faLock = faLock;
  constructor(private authService: AuthService) {}
  isTeacher$ = this.authService.hasRole('Teacher');
}
