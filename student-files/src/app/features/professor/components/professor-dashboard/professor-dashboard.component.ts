import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import {
  BaseComponent,
  IBaseResult,
  IResult,
  ISimpleUserDto,
  IUserDto,
  StudentFilesConstants,
  StudentFilesFormValidators,
  ToasterMessages,
  UserRole,
  UserRolePipe,
} from '../../../../shared';
import {
  AuthService,
  IUserDetailsFromToken,
  ToasterService,
  ToasterType,
} from '../../../../core';
import { catchError, of, switchMap } from 'rxjs';
import { ProfessorService } from '../../services/professor.service';
import { ICourseDto } from '../../contracts/ICourseDto';
import { ISubmitGradeRequest } from '../../contracts/ISubmitGradeRequest';
import { ResolveStart } from '@angular/router';

@Component({
  selector: 'app-professor-dashboard',
  imports: [CommonModule, MatFormFieldModule, ReactiveFormsModule],
  templateUrl: './professor-dashboard.component.html',
  styleUrl: './professor-dashboard.component.scss',
})
export class ProfessorDashboardComponent
  extends BaseComponent
  implements OnInit
{
  public currentPofessorUid: string = '';

  public submitStudentGradeForm!: FormGroup;
  public students: ISimpleUserDto[] = [];
  public courses: ICourseDto[] = [];

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly authService: AuthService,
    private readonly professorService: ProfessorService,
    private readonly toasterService: ToasterService
  ) {
    super();
  }

  public onCancelClicked(): void {
    this.submitStudentGradeForm.reset();
  }

  public onSubmitClicked(): void {
    const request: ISubmitGradeRequest = {
      studentUid: this.submitStudentGradeForm.controls['student'].value,
      professorUid: this.currentPofessorUid,
      courseUid: this.submitStudentGradeForm.controls['course'].value,
      grade: this.submitStudentGradeForm.controls['grade'].value as number,
    };

    this.professorService
      .submitGrade(request)
      .pipe(
        this.untilDestroyed(),
        catchError((response) => {
          this.toasterService.show(
            response.error.error?.message || ToasterMessages.COMMON_ERROR,
            'error'
          );

          return of(null);
        })
      )
      .subscribe((result: IBaseResult | null) => {
        if (result && result.isSuccess) {
          this.toasterService.show(
            ToasterMessages.PROFESSOR_SUBMIT_GRADE_SUCCESSFUL,
            'success'
          );

          this.submitStudentGradeForm.reset();
        }
      });
  }

  public ngOnInit(): void {
    this.initForm();

    this.authService.currentUser$
      .pipe(
        this.untilDestroyed(),
        switchMap((currentUser: IUserDetailsFromToken | null) => {
          if (currentUser && currentUser.userRole === UserRole.Professor) {
            this.currentPofessorUid = currentUser.userUid;

            return this.professorService
              .getProfessorCourses(currentUser.userUid)
              .pipe(this.untilDestroyed());
          }

          return of(null);
        }),
        catchError((error) => {
          this.toasterService.show(
            error.error.error?.message || ToasterMessages.COMMON_ERROR,
            'error'
          );

          return of(undefined);
        })
      )
      .subscribe((response: IResult<ICourseDto[]> | null | undefined) => {
        if (response) {
          if (response.isSuccess && response.value) {
            if (response.value.length === 0) {
              this.toasterService.show(
                ToasterMessages.PROFESSOR_NO_COURSES_FOUND,
                ToasterType.Warning
              );
            } else {
              this.courses = response.value;
            }
          }
        } else if (response === null) {
          this.toasterService.show(
            ToasterMessages.COMMON_ERROR,
            ToasterType.Error
          );
        }
      });

    this.professorService
      .getAllStudents()
      .pipe(
        this.untilDestroyed(),
        catchError((error) => {
          this.toasterService.show(
            error.error.error?.message || ToasterMessages.COMMON_ERROR,
            'error'
          );

          return of(null);
        })
      )
      .subscribe((studentsResult: IResult<ISimpleUserDto[]> | null) => {
        if (
          studentsResult &&
          studentsResult.isSuccess &&
          studentsResult.value
        ) {
          this.students = studentsResult.value;
        }
      });
  }

  private initForm(): void {
    this.submitStudentGradeForm = this.formBuilder.group({
      student: ['', [Validators.required]],
      course: ['', [Validators.required]],
      grade: ['', [Validators.required]],
    });
  }
}
