import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IGradeDto } from '../../contracts/IGradeDto';
import {
  BaseComponent,
  IResult,
  ToasterMessages,
  UserRole,
} from '../../../../shared';
import {
  AuthService,
  IUserDetailsFromToken,
  ToasterService,
  ToasterType,
} from '../../../../core';
import { catchError, of, switchMap } from 'rxjs';
import { StudentService } from '../../services/student.service';
import { DateFormatPipe } from '../../../../shared';
import { UserNamePipe } from '../../../../shared';

@Component({
  selector: 'app-student-dashboard',
  imports: [CommonModule, DateFormatPipe, UserNamePipe],
  templateUrl: './student-dashboard.component.html',
  styleUrl: './student-dashboard.component.scss',
})
export class StudentDashboardComponent extends BaseComponent implements OnInit {
  public studentGrades: IGradeDto[] = [];

  constructor(
    private readonly authService: AuthService,
    private readonly studentService: StudentService,
    private readonly toasterService: ToasterService
  ) {
    super();
  }

  public ngOnInit(): void {
    this.authService.currentUser$
      .pipe(
        this.untilDestroyed(),
        switchMap((currentUser: IUserDetailsFromToken | null) => {
          if (currentUser && currentUser.userRole === UserRole.Student) {
            return this.studentService.getStudentGrades(currentUser.userUid);
          } else {
            this.toasterService.show(
              ToasterMessages.COMMON_ERROR,
              ToasterType.Error
            );

            return of(null);
          }
        }),
        catchError((error) => {
          this.toasterService.show(
            error.error.error?.message || ToasterMessages.COMMON_ERROR,
            ToasterType.Error
          );

          return of(null);
        })
      )
      .subscribe((studentGradesResult: IResult<IGradeDto[]> | null) => {
        if (
          studentGradesResult &&
          studentGradesResult.isSuccess &&
          studentGradesResult.value
        ) {
          this.studentGrades = studentGradesResult.value;
        }
      });
  }
}
