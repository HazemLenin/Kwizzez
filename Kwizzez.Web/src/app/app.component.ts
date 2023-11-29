import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { initFlowbite } from 'flowbite';
import { DarkModeService } from './services/dark-mode.service';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Kwizzez.Web';
  constructor(
    private darkModeService: DarkModeService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    initFlowbite();
    this.darkModeService.setDarkModeClass(this.darkModeService.isDarkModeOn());
    this.authService.isAuthenticated().subscribe((isAuthenticated) => {
      if (isAuthenticated) {
        this.authService.UpdateUser();
      }
    });
  }
}
