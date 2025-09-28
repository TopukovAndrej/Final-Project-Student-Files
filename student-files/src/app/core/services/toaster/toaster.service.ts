import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ToasterType } from '../../contracts';

@Injectable({
  providedIn: 'root',
})
export class ToasterService {
  private readonly toasterDuration = 3000;

  constructor(private readonly snackBar: MatSnackBar) {}

  show(message: string, type: ToasterType = 'info'): void {
    this.snackBar.open(message, 'Close', {
      duration: this.toasterDuration,
      verticalPosition: 'bottom',
      horizontalPosition: 'right',
      panelClass: [`toast-${type}`],
    });
  }
}
