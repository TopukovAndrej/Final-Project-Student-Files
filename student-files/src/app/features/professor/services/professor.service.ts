import { Injectable } from '@angular/core';
import { HttpService } from '../../../core';
import { Observable } from 'rxjs';
import { IBaseResult, IResult, ISimpleUserDto } from '../../../shared';
import { ICourseDto } from '../contracts/ICourseDto';
import { ISubmitGradeRequest } from '../contracts/ISubmitGradeRequest';

@Injectable({
  providedIn: 'root',
})
export class ProfessorService {
  constructor(private readonly httpService: HttpService) {}

  public getProfessorCourses(
    professorUid: string
  ): Observable<IResult<ICourseDto[]>> {
    return this.httpService.get<IResult<ICourseDto[]>>(
      `/courses/professor/${professorUid}`
    );
  }

  public getAllStudents(): Observable<IResult<ISimpleUserDto[]>> {
    return this.httpService.get<IResult<ISimpleUserDto[]>>(
      '/users/all-students'
    );
  }

  public submitGrade(request: ISubmitGradeRequest): Observable<IBaseResult> {
    return this.httpService.post<IBaseResult>('/grades/submit', request);
  }
}
