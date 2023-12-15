import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { inject } from '@angular/core';
import { tap } from 'rxjs';

export const teacherGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  return authService.hasRole('Teacher').pipe(
    tap((isTeacher) => {
      return !isTeacher
        ? router.navigate(['/forbidden'], {
            skipLocationChange: true,
          })
        : true;
    })
  );
};
