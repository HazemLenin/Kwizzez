import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { AuthService } from 'src/app/services/auth.service';
import { login } from 'src/app/states/isAuthenticated/isAuthenticated.actions';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
})
export class SignupComponent {
  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private store: Store<{ isAuthenticated: boolean }>,
    private router: Router
  ) {}

  signupForm = this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
    passwordConfirmation: ['', Validators.required],
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    userName: ['', Validators.required],
    dateOfBirth: ['', Validators.required],
    isTeacher: [false],
  });

  errors: string[] = [];

  get email() {
    return this.signupForm.get('email');
  }
  get password() {
    return this.signupForm.get('password');
  }
  get passwordConfirmation() {
    return this.signupForm.get('passwordConfirmation');
  }
  get firstName() {
    return this.signupForm.get('firstName');
  }
  get lastName() {
    return this.signupForm.get('lastName');
  }
  get userName() {
    return this.signupForm.get('userName');
  }
  get dateOfBirth() {
    return this.signupForm.get('dateOfBirth');
  }
  get isTeacher() {
    return this.signupForm.get('isTeacher');
  }

  signup() {
    this.errors = [];
    this.signupForm.markAllAsTouched();
    if (this.password?.value !== this.passwordConfirmation?.value) {
      this.errors = ["Passwords don't match."];
    } else if (this.signupForm.valid) {
      this.authService
        .signup({
          email: this.email?.value ?? '',
          password: this.password?.value ?? '',
          firstName: this.firstName?.value ?? '',
          lastName: this.lastName?.value ?? '',
          userName: this.userName?.value ?? '',
          dateOfBirth: new Date(this.dateOfBirth?.value ?? ''),
          isTeacher: this.isTeacher?.value ?? false,
        })
        .subscribe((response) => {
          if (response.isSucceed) {
            localStorage.setItem('token', response.data.token);
            this.store.dispatch(login());
            this.router.navigate(['']);
          } else {
            this.errors = Object.values(response.errors);
          }
        });
    }
  }
}
