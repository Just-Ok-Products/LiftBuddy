import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar'

@Injectable({
  providedIn: 'root'
})
export class SnackBarService {

  constructor(
    private snackBar: MatSnackBar
  ) { }

  private defaultSnackbarSettings: MatSnackBarConfig = {
    duration: 2000,
    horizontalPosition: 'end',
    verticalPosition: 'top'
  };

  public openSnackbar(message: string, action: string | undefined = undefined) {
    this.snackBar.open(message, action, this.defaultSnackbarSettings);
  }

  public openSuccessSnackbar(message: string, action: string | undefined = undefined) {
    let modifiedsettings = this.defaultSnackbarSettings;
    modifiedsettings.panelClass = ["success-snackbar"];
    this.snackBar.open(message, action, modifiedsettings);
  }

  public operErrorSnackbar(message: string, action: string | undefined = undefined) {
    let modifiedsettings = this.defaultSnackbarSettings;
    modifiedsettings.panelClass = ["error-snackbar"];
    modifiedsettings.duration = 5000;
    this.snackBar.open(message, action, modifiedsettings);
  }

}
