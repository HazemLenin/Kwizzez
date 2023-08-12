import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent {
  isAuthenticated$: Observable<boolean>;

  constructor(private store: Store<{ isAuthenticated: boolean }>) {
    this.isAuthenticated$ = store.select('isAuthenticated');
  }
}
