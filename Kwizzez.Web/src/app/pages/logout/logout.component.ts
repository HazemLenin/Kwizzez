import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { AuthService } from 'src/app/services/auth.service';
import { logout } from 'src/app/states/tokens/tokens.actions';
import { removeUser } from 'src/app/states/user/user.actions';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css'],
})
export class LogoutComponent implements OnInit {
  constructor(private store: Store, private router: Router) {}

  ngOnInit(): void {
    this.store.dispatch(logout());
    this.store.dispatch(removeUser());
    this.router.navigate(['']);
  }
}
