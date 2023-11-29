import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StoreModule } from '@ngrx/store';
import { LoginComponent } from './pages/login/login.component';
import { SignupComponent } from './pages/signup/signup.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HomeComponent } from './pages/home/home.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ReactiveFormsModule } from '@angular/forms';
import { userReducer } from './states/user/user.reducers';
import { tokensReducer } from './states/tokens/tokens.reducers';
import { LogoutComponent } from './pages/logout/logout.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { ToastrModule } from 'ngx-toastr';
import { TokenRefreshInterceptor } from './interceptors/token-refresh.interceptor';
import { UnauthorizedComponent } from './pages/unauthorized/unauthorized.component';
import { QuizComponent } from './components/home/quiz/quiz.component';
import { FooterComponent } from './components/footer/footer.component';
import { MyQuizzesComponent } from './pages/my-quizzes/my-quizzes.component';
import { ForbiddenComponent } from './pages/forbidden/forbidden.component';
import { AddQuizComponent } from './pages/add-quiz/add-quiz.component';
import { TestComponent } from './pages/test/test.component';
import { EditQuizComponent } from './pages/edit-quiz/edit-quiz.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SignupComponent,
    NavbarComponent,
    HomeComponent,
    LogoutComponent,
    NotFoundComponent,
    UnauthorizedComponent,
    QuizComponent,
    FooterComponent,
    MyQuizzesComponent,
    ForbiddenComponent,
    AddQuizComponent,
    TestComponent,
    EditQuizComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    StoreModule.forRoot({
      user: userReducer,
      tokens: tokensReducer,
    }),
    FontAwesomeModule,
    ReactiveFormsModule,
    HttpClientModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    FontAwesomeModule,
    ReactiveFormsModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenRefreshInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
