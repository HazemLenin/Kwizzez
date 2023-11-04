import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { AuthService } from 'src/app/services/auth.service';
import { login } from 'src/app/states/isAuthenticated/isAuthenticated.actions';
import { faCircleNotch } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private store: Store<{ isAuthenticated: boolean }>,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  redirectUrl: String = '';
  faCircleNotch = faCircleNotch;
  loading: boolean = false;

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.redirectUrl = params['redirectUrl'] ?? '';
    });
  }

  loginForm = this.formBuilder.group({
    email: ['', Validators.required],
    password: ['', Validators.required],
  });

  errors: string[] = [];

  get email() {
    return this.loginForm.get('email');
  }
  get password() {
    return this.loginForm.get('password');
  }

  login() {
    this.errors = [];
    this.loginForm.markAllAsTouched();

    if (this.loginForm.valid) {
      this.loading = true;
      this.email?.disable();
      this.password?.disable();
      this.authService
        .login(this.email?.value ?? '', this.password?.value ?? '')
        .subscribe((response) => {
          this.loading = false;
          this.email?.enable();
          this.password?.enable();
          if (response.isSucceed) {
            localStorage.setItem('token', response.data.token);
            localStorage.setItem('refreshToken', response.data.refreshToken);
            this.store.dispatch(login());
            this.router.navigate([this.redirectUrl]);
          } else {
            this.errors = Object.values(response.errors);
          }
        });
    }
  }
}
