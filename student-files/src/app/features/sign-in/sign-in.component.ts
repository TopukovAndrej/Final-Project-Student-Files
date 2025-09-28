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
import { Router, RouterModule } from '@angular/router';
import { BaseComponent, StudentFilesFormValidators } from '../../shared';
import { AuthService, IUserLoginRequest } from '../../core';

@Component({
  selector: 'app-sign-in',
  imports: [
    MatIconModule,
    MatButtonModule,
    ReactiveFormsModule,
    CommonModule,
    MatFormFieldModule,
    RouterModule,
  ],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.scss',
})
export class SignInComponent extends BaseComponent implements OnInit {
  public signInForm!: FormGroup;

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly authService: AuthService,
    private readonly router: Router
  ) {
    super();
  }

  public onCancelClicked(): void {
    this.signInForm.controls['username'].reset();
    this.signInForm.controls['password'].reset();
  }

  public onSignInClicked(): void {
    console.log('Sign In clicked');

    const signInFormValues = this.signInForm.value;

    const request: IUserLoginRequest = {
      username: this.signInForm.controls['username'].value,
      password: this.signInForm.controls['password'].value,
    };

    this.authService
      .authenticateUser(request)
      .pipe(this.untilDestroyed())
      .subscribe({
        next: (result) => {
          if (result) {
            this.router.navigate(['/home']);
          }
        },
        error: (error) => {
          console.error('Unexpected error: ', error);
        },
      });
  }

  public ngOnInit(): void {
    this.initForm();
  }

  private initForm(): void {
    this.signInForm = this.formBuilder.group({
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
