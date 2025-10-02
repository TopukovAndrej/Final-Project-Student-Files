import { Component, OnInit } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
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
  IUserDto,
  StudentFilesConstants,
  StudentFilesErrorMessages,
  StudentFilesFormValidators,
  ToasterMessages,
  UserRolePipe,
} from '../../../../shared';
import { AdminService } from '../../services/admin.service';
import { ToasterService, ToasterType } from '../../../../core';
import { ICreateUserRequest } from '../..';
import { catchError, of, switchMap } from 'rxjs';

@Component({
  selector: 'app-admin-dashboard',
  imports: [
    CommonModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    UserRolePipe,
  ],
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.scss',
})
export class AdminDashboardComponent extends BaseComponent implements OnInit {
  public createUserForm!: FormGroup;
  public users: IUserDto[] = [];

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly adminService: AdminService,
    private readonly toasterService: ToasterService
  ) {
    super();
  }

  public ngOnInit(): void {
    this.initForm();

    this.adminService
      .getAllNonAdminUsers()
      .pipe(this.untilDestroyed())
      .subscribe({
        next: (result: IResult<IUserDto[]>) => {
          if (result.isSuccess && result.value) {
            this.users = result.value;
          }
        },
        error: (response) => {
          this.toasterService.show(
            response.error ?? ToasterMessages.COMMON_ERROR,
            ToasterType.Error
          );
        },
      });
  }

  public onDeleteClicked(userUid: string): void {
    if (confirm(StudentFilesConstants.DeleteConfirmationMessage)) {
      this.adminService
        .deleteUser(userUid)
        .pipe(this.untilDestroyed())
        .subscribe((result: IBaseResult) => {
          if (result.isSuccess) {
            this.users = this.users.filter((x) => x.uid !== userUid);
            this.toasterService.show(
              ToasterMessages.ADMIN_DELETE_USER_SUCCESSFUL,
              ToasterType.Success
            );
          } else {
            this.toasterService.show(
              ToasterMessages.ADMIN_DELETE_USER_FAILED,
              ToasterType.Error
            );
          }
        });
    }
  }

  public onCreateClicked(): void {
    if (
      confirm(StudentFilesConstants.CreateConfirmationMessage) &&
      this.createUserForm.valid
    ) {
      const request: ICreateUserRequest = {
        username: this.createUserForm.controls['username'].value as string,
        password: this.createUserForm.controls['password'].value as string,
        role: this.createUserForm.controls['userRole'].value as string,
      };

      this.adminService
        .createUser(request)
        .pipe(
          this.untilDestroyed(),
          switchMap((createUserResult: IResult<string>) => {
            if (createUserResult.isSuccess && createUserResult.value) {
              return this.adminService.getUserByUid(createUserResult.value);
            } else {
              return of(createUserResult);
            }
          }),
          catchError((error) => {
            this.toasterService.show(
              error.error.error?.message || ToasterMessages.COMMON_ERROR,
              'error'
            );

            return of(null);
          })
        )
        .subscribe((result: IResult<string> | IResult<IUserDto> | null) => {
          if (
            result &&
            result.isSuccess &&
            result.value &&
            typeof result.value !== 'string'
          ) {
            this.users.push(result.value);

            this.toasterService.show(
              ToasterMessages.ADMIN_CREATE_USER_SUCCESSFUL,
              'success'
            );
          } else if (result == null) {
            return;
          } else {
            this.toasterService.show(
              result.error?.message || ToasterMessages.COMMON_ERROR,
              'error'
            );
          }
        });
    }
  }

  public onCancelClicked(): void {
    this.createUserForm.reset();
  }

  private initForm(): void {
    this.createUserForm = this.formBuilder.group({
      userRole: ['', [Validators.required]],
      username: ['', [Validators.required, Validators.email]],
      password: [
        '',
        [
          Validators.required,
          StudentFilesFormValidators.signInPasswordFormControlValidator(),
        ],
      ],
    });
  }
}
