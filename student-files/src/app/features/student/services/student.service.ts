import { Injectable } from '@angular/core';
import { HttpService } from '../../../core';
import { Observable } from 'rxjs';
import { IResult } from '../../../shared';
import { IGradeDto } from '../contracts/IGradeDto';

@Injectable({
  providedIn: 'root',
})
export class StudentService {
  constructor(private readonly httpService: HttpService) {}

  public getStudentGrades(
    studentUid: string
  ): Observable<IResult<IGradeDto[]>> {
    return this.httpService.get<IResult<IGradeDto[]>>(
      `/grades/student/${studentUid}`
    );
  }
}
