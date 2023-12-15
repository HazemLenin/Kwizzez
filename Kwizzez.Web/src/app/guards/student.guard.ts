import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { inject } from '@angular/core';
import { tap } from 'rxjs';

export const studentGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  return authService.hasRole('Student').pipe(
    tap((isStudent) => {
      return !isStudent
        ? router.navigate(['/forbidden'], {
            skipLocationChange: true,
          })
        : true;
    })
  );
};
