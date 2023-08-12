import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { logout } from 'src/app/states/isAuthenticated/isAuthenticated.actions';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css'],
})
export class LogoutComponent implements OnInit {
  constructor(
    private store: Store<{ isAuthenticated: boolean }>,
    private router: Router
  ) {}

  ngOnInit(): void {
    localStorage.removeItem('token');
    this.store.dispatch(logout());
    this.router.navigate(['']);
  }
}
