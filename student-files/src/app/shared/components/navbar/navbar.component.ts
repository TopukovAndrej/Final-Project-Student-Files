import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { UserRolePipe } from '../../pipes/user-role/user-role.pipe';
import { Router } from '@angular/router';
import { AuthService, IUserDetailsFromToken } from '../../../core';
import { BaseComponent } from '../base/base.component';

@Component({
  selector: 'app-navbar',
  imports: [MatButtonModule, CommonModule, UserRolePipe],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent extends BaseComponent implements OnInit {
  public signedInUserRole: string = '';

  constructor(
    private readonly router: Router,
    private readonly authService: AuthService
  ) {
    super();
  }

  public onSignInClicked(): void {
    this.router.navigate(['/sign-in']);
  }

  public onSignOutClicked(): void {
    this.authService.signOut();
    this.signedInUserRole = '';
  }

  public ngOnInit(): void {
    this.authService.currentUser$
      .pipe(this.untilDestroyed())
      .subscribe((user: IUserDetailsFromToken | null) => {
        if (user) {
          this.signedInUserRole = user.userRole;
        } else {
          this.signedInUserRole = '';
        }
      });
  }
}
