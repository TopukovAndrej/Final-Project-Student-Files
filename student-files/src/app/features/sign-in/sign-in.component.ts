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
import { RouterModule } from '@angular/router';
import { StudentFilesFormValidators } from '../../shared';

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
export class SignInComponent implements OnInit {
  public signInForm!: FormGroup;

  constructor(private readonly formBuilder: FormBuilder) {}

  public onCancelClicked(): void {
    this.signInForm.controls['username'].reset();
    this.signInForm.controls['password'].reset();
  }

  public onSignInClicked(): void {
    console.log('Sign In clicked');
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
