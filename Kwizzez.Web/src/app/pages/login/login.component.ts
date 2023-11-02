import { Component } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { AuthService } from 'src/app/services/auth.service';
import { login } from 'src/app/states/isAuthenticated/isAuthenticated.actions';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private store: Store<{ isAuthenticated: boolean }>,
    private router: Router
  ) {}

  loginForm = this.formBuilder.group({
    email: ['', Validators.required],
    password: ['', Validators.required],
  });

  error: string = '';

  get email() {
    return this.loginForm.get('email');
  }
  get password() {
    return this.loginForm.get('password');
  }

  login() {
    if (this.loginForm.valid) {
      this.authService
        .login(this.email?.value ?? '', this.password?.value ?? '')
        .subscribe((response) => {
          if (response.isSucceed) {
            localStorage.setItem('token', response.data.token);
            localStorage.setItem('refreshToken', response.data.refreshToken);
            this.store.dispatch(login());
            this.router.navigate(['']);
          } else {
            this.error = response.errors[''];
          }
        });
    }
  }
}
