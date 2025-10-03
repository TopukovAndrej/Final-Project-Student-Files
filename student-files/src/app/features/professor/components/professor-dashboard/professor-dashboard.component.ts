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
  UserRolePipe,
} from '../../../../shared';
import { ToasterService, ToasterType } from '../../../../core';
import { catchError, of, switchMap } from 'rxjs';
import { ProfessorServiceService } from '../../services/professor.service';
import { ICourseDto } from '../../contracts/ICourseDto';

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
  public submitStudentGradeForm!: FormGroup;
  public students: ISimpleUserDto[] = [];
  public courses: ICourseDto[] = [];

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly professorService: ProfessorServiceService,
    private readonly toasterService: ToasterService
  ) {
    super();
  }

  public onCancelClicked(): void {
    this.submitStudentGradeForm.reset();
  }

  public onSubmitClicked(): void {
    throw new Error('Method not implemented.');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  private initForm(): void {
    this.submitStudentGradeForm = this.formBuilder.group({
      student: ['', [Validators.required]],
      course: ['', [Validators.required]],
      grade: ['', [Validators.required]],
    });
  }
}
