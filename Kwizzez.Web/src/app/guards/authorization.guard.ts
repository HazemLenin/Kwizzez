import { CanActivateFn, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { AuthService } from '../services/auth.service';
import { inject } from '@angular/core';
import { lastValueFrom, map, tap } from 'rxjs';

export const authorizationGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  return authService.isAuthenticated().pipe(
    tap((isAuthenticated) => {
      return !isAuthenticated
        ? router.navigate(['/unauthorized'], { skipLocationChange: true })
        : true;
    })
  );
};
