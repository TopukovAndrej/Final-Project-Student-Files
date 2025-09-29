import { Injectable } from '@angular/core';
import { HttpService } from '../../../core';
import { Observable } from 'rxjs';
import { IResult, IUserDto } from '../../../shared';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  constructor(private readonly httpService: HttpService) {}

  public getAllNonAdminUsers(): Observable<IResult<IUserDto[]>> {
    return this.httpService.get<IResult<IUserDto[]>>('/users/all-non-admin');
  }
}
