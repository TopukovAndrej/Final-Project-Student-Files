import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { UserRolePipe } from '../../pipes/user-role/user-role.pipe';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  imports: [MatButtonModule, CommonModule, UserRolePipe],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent {
  public signedInUserRole: string = '';

  constructor(private readonly router: Router) {}

  public onSignInClicked(): void {
    this.router.navigate(['/sign-in']);
  }
}
