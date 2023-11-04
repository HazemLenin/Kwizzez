import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { DarkModeService } from 'src/app/services/dark-mode.service';
import { faMoon, faSun } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent {
  isAuthenticated$: Observable<boolean>;

  constructor(
    private authService: AuthService,
    private darkModeService: DarkModeService
  ) {
    this.isAuthenticated$ = authService.isAuthenticated();
  }

  faMoon = faMoon;
  faSun = faSun;

  toggleDarkMode() {
    this.darkModeService.toggleDarkMode();
  }

  isDarkMode() {
    return this.darkModeService.isDarkModeOn();
  }
}
