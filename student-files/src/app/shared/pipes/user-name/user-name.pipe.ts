import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'userName',
})
export class UserNamePipe implements PipeTransform {
  transform(value: string, role: string, ...args: unknown[]): unknown {
    if (!value) {
      return '';
    }

    const usernamePart = value.split('@')[0];
    const nameSegment = usernamePart.split('.')[0];
    const formattedName =
      nameSegment.charAt(0).toUpperCase() + nameSegment.slice(1);

    return `${role} ${formattedName}`;
  }
}
