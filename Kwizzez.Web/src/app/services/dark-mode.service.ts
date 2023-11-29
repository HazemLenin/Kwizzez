import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DarkModeService {
  private isDarkMode = JSON.parse(localStorage.getItem('darkMode') ?? 'false');

  toggleDarkMode() {
    this.isDarkMode = !this.isDarkMode;
    this.setDarkModeClass(this.isDarkMode);
  }

  setDarkModeClass(isDarkMode: boolean) {
    document.documentElement.classList.toggle('dark', isDarkMode);
    localStorage.setItem('darkMode', JSON.stringify(isDarkMode));
  }

  isDarkModeOn() {
    return this.isDarkMode;
  }
}
