import {
  HttpInterceptorFn,
  HttpRequest,
  HttpErrorResponse,
} from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { AuthService } from '../../services/auth/auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const authReq = addTokenToRequest(req, authService);

  return next(authReq).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401 && !isAuthRequest(req.url)) {
        authService.signOut();
        router.navigate(['/home']);
      }
      return throwError(() => error);
    })
  );
};

function addTokenToRequest(
  req: HttpRequest<unknown>,
  authService: AuthService
): HttpRequest<unknown> {
  const token = authService.getToken();

  if (!token || isAuthRequest(req.url)) {
    return req;
  }

  return req.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`,
    },
  });
}

function isAuthRequest(url: string): boolean {
  return url.includes('/auth/');
}
