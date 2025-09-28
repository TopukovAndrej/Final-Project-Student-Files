import { Injectable } from '@angular/core';
import { HttpService } from '../http/http.service';
import { BehaviorSubject, catchError, Observable, of, switchMap } from 'rxjs';
import {
  IDecodedUserToken,
  IUserDetailsFromToken,
  IUserLoginRequest,
  IUserLoginResponse,
  ToasterType,
} from '../../contracts';
import { Router } from '@angular/router';
import { IResult, ToasterMessages } from '../../../shared';
import { ToasterService } from '../toaster/toaster.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly tokenKey = 'currentUser';
  private currentUserSubject =
    new BehaviorSubject<IUserDetailsFromToken | null>(null);

  public currentUser$ = this.currentUserSubject.asObservable();

  constructor(
    private readonly httpService: HttpService,
    private readonly router: Router,
    private readonly toasterService: ToasterService
  ) {
    this.currentUserSubject.next(this.fetchUserDetailsFromToken());
  }

  authenticateUser(request: IUserLoginRequest): Observable<boolean> {
    return this.httpService
      .post<IResult<IUserLoginResponse>>('/auth/login', request)
      .pipe(
        switchMap((result) => {
          if (result.isSuccess && result.value) {
            this.setJwtToken(result.value.token);
            this.currentUserSubject.next(this.fetchUserDetailsFromToken());
            this.toasterService.show(
              ToasterMessages.AUTH_LOGIN_SUCCESSFUL,
              ToasterType.Success
            );
            return of(true);
          }

          return of(false);
        }),
        catchError((response) => {
          this.toasterService.show(
            response.error.error.message ?? ToasterMessages.COMMON_ERROR,
            ToasterType.Error
          );

          return of(false);
        })
      );
  }

  isSessionExpired(tokenExpirationClaim: number): boolean {
    return Date.now() >= tokenExpirationClaim * 1000;
  }

  signOut(): void {
    localStorage.removeItem(this.tokenKey);

    this.clearAuthState();

    this.router.navigate(['/home']);

    this.toasterService.show('Signed out successfully', 'success');
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  private setJwtToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  private clearAuthState(): void {
    localStorage.removeItem(this.tokenKey);
    this.currentUserSubject.next(null);
  }

  private fetchUserDetailsFromToken(): IUserDetailsFromToken | null {
    const token = localStorage.getItem(this.tokenKey);

    if (token == null) {
      return null;
    }

    try {
      const payload = token.split('.')[1];

      const decodedData = JSON.parse(atob(payload)) as IDecodedUserToken;

      if (this.isSessionExpired(decodedData.exp)) {
        this.clearAuthState();

        return null;
      }

      return {
        userUid: decodedData.sub,
        username: decodedData.email,
        userRole: decodedData.role,
      };
    } catch (error) {
      console.error('Cannot decode token, error: ', error);

      this.clearAuthState();

      return null;
    }
  }
}
