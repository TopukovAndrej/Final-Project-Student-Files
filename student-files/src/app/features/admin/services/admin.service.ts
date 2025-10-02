import { Injectable } from '@angular/core';
import { HttpService } from '../../../core';
import { Observable } from 'rxjs';
import { IBaseResult, IResult, IUserDto } from '../../../shared';
import { ICreateUserRequest } from '..';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  constructor(private readonly httpService: HttpService) {}

  public getAllNonAdminUsers(): Observable<IResult<IUserDto[]>> {
    return this.httpService.get<IResult<IUserDto[]>>('/users/all-non-admin');
  }

  public deleteUser(userUid: string): Observable<IBaseResult> {
    return this.httpService.delete<IBaseResult>(
      `/users/delete-user/${userUid}`
    );
  }

  public createUser(request: ICreateUserRequest): Observable<IResult<string>> {
    return this.httpService.post<IResult<string>>(
      `/users/create-user`,
      request
    );
  }

  public getUserByUid(userUid: string): Observable<IResult<IUserDto>> {
    return this.httpService.get<IResult<IUserDto>>(
      `/users/get-user/${userUid}`
    );
  }
}
