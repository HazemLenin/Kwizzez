import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { SignupComponent } from './pages/signup/signup.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { LogoutComponent } from './pages/logout/logout.component';
import { authorizationGuard } from './guards/authorization.guard';
import { UnauthorizedComponent } from './pages/unauthorized/unauthorized.component';
import { QuizComponent } from './pages/quiz/quiz.component';
import { MyQuizzesComponent } from './pages/my-quizzes/my-quizzes.component';
import { teacherGuard } from './guards/teacher.guard';
import { ForbiddenComponent } from './pages/forbidden/forbidden.component';
import { AddQuizComponent } from './pages/add-quiz/add-quiz.component';
import { TestComponent } from './pages/test/test.component';
import { studentGuard } from './guards/student.guard';
import { EditQuizComponent } from './pages/edit-quiz/edit-quiz.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'signup',
    component: SignupComponent,
  },
  {
    path: 'logout',
    component: LogoutComponent,
  },
  {
    path: 'quizzes/:id',
    component: QuizComponent,
    canActivate: [authorizationGuard, studentGuard],
  },
  {
    path: 'my-quizzes',
    component: MyQuizzesComponent,
    canActivate: [authorizationGuard, teacherGuard],
  },
  {
    path: 'add-quiz',
    component: AddQuizComponent,
    canActivate: [authorizationGuard, teacherGuard],
  },
  {
    path: 'edit-quiz/:id',
    component: EditQuizComponent,
    canActivate: [authorizationGuard, teacherGuard],
  },
  {
    path: 'test',
    component: TestComponent,
  },
  {
    path: 'unauthorized',
    component: UnauthorizedComponent,
  },
  {
    path: 'forbidden',
    component: ForbiddenComponent,
  },
  {
    path: 'notFound',
    component: NotFoundComponent,
  },
  {
    path: '**',
    component: NotFoundComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
