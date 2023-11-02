import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, catchError, switchMap, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable()
export class TokenRefreshInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((err) => {
        if (err.status == 401) {
          return this.authService.refreshToken().pipe(
            switchMap((response: any) => {
              this.authService.setAccessToken(response.data.token);
              this.authService.setRefreshToken(response.data.refreshToken);
              const newRequest = request.clone({
                setHeaders: {
                  Authorization: `Bearer ${this.authService.getAccessToken()}`,
                },
              });
              return next.handle(newRequest);
            })
          );
        }
        return throwError(err);
      })
    );
  }
}
