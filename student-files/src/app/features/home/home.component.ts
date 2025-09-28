import { Component, OnInit } from '@angular/core';
import { BaseComponent, StudentFilesConstants, UserRole } from '../../shared';
import { AuthService, IUserDetailsFromToken } from '../../core';

@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent extends BaseComponent implements OnInit {
  public contentMessage: string = StudentFilesConstants.PleaseLoginMessage;

  constructor(private readonly authService: AuthService) {
    super();
  }

  public ngOnInit(): void {
    this.authService.currentUser$
      .pipe(this.untilDestroyed())
      .subscribe((user: IUserDetailsFromToken | null) => {
        if (user) {
          if (user.role === UserRole.Admin) {
            this.contentMessage = StudentFilesConstants.WelcomeAdminMessage;
          } else {
            const userName = user.username.split('.')[0];

            const name = userName.charAt(0).toUpperCase() + userName.slice(1);

            if (user.role === UserRole.Professor) {
              this.contentMessage = `Welcome, professor ${name}! Have a productive day!`;
            } else {
              this.contentMessage = `Welcome, ${name}! Have a good day!`;
            }
          }
        } else {
          this.contentMessage = StudentFilesConstants.PleaseLoginMessage;
        }
      });
  }
}
