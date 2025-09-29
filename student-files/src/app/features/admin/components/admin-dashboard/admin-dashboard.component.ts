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
  IResult,
  IUserDto,
  StudentFilesFormValidators,
  ToasterMessages,
  UserRolePipe,
} from '../../../../shared';
import { AdminService } from '../../services/admin.service';
import { ToasterService, ToasterType } from '../../../../core';

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
  public userManagementForm!: FormGroup;
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
            response.error.error.message ?? ToasterMessages.COMMON_ERROR,
            ToasterType.Error
          );
        },
      });
  }

  private initForm(): void {
    this.userManagementForm = this.formBuilder.group({
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
