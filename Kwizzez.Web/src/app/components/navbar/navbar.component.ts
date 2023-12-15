import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { DarkModeService } from 'src/app/services/dark-mode.service';
import { faMoon, faSun } from '@fortawesome/free-solid-svg-icons';
import { User } from 'src/app/models/User';
import Tokens from 'src/app/models/Tokens';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent {
  user$: Observable<User>;
  isAuthenticated$: Observable<boolean>;
  isTeacher$: Observable<boolean>;

  constructor(
    private authService: AuthService,
    private darkModeService: DarkModeService
  ) {
    this.isAuthenticated$ = authService.isAuthenticated();
    this.isTeacher$ = authService.hasRole('Teacher');
    this.user$ = authService.getUser();
  }

  faMoon = faMoon;
  faSun = faSun;

  toggleDarkMode(): void {
    this.darkModeService.toggleDarkMode();
  }

  isDarkMode(): boolean {
    return this.darkModeService.isDarkModeOn();
  }
}
