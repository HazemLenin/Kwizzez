import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, catchError, switchMap, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { Store } from '@ngrx/store';
import Tokens from '../models/Tokens';
import { login } from '../states/tokens/tokens.actions';

@Injectable()
export class TokenRefreshInterceptor implements HttpInterceptor {
  constructor(
    private authService: AuthService,
    private store: Store<{ tokens: Tokens }>
  ) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((err) => {
        if (err.status == 401) {
          return this.authService.refreshTokens().pipe(
            switchMap((response: any) => {
              this.store.dispatch(login({ payload: response.data }));
              const newRequest = request.clone({
                setHeaders: {
                  Authorization: `Bearer ${response.data.token}`,
                },
              });
              return next.handle(newRequest);
            })
          );
        }
        return throwError(() => err);
      })
    );
  }
}
