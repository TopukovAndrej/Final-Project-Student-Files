import { Pipe, PipeTransform } from '@angular/core';
import { UserRole } from '../../contracts/models/user-role.model';

@Pipe({
  name: 'userRole',
})
export class UserRolePipe implements PipeTransform {
  transform(value: string): string {
    switch (value) {
      case UserRole.Student:
        return 'Student';
      case UserRole.Professor:
        return 'Professor';
      case UserRole.Admin:
        return 'Admin';
      default:
        return '';
    }
  }
}
