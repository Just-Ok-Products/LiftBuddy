import { LoadingVisualizationService } from './../../../../../../Services/loading-visualization.service';
import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/Pages/Components/confirmation-dialog/confirmation-dialog.component';
import { GeneralSettingsComponent } from '../../general-settings.component';

@Component({
  selector: 'app-swap-role-confirmation-dialog',
  templateUrl: './swap-role-confirmation-dialog.component.html',
  styleUrls: ['./swap-role-confirmation-dialog.component.scss']
})
export class SwapRoleConfirmationDialogComponent {

  constructor(
    private dialogRef: MatDialogRef<SwapRoleConfirmationDialogComponent>,
    private loadingVisService: LoadingVisualizationService
  ) {}

  public cancel() {
    ConfirmationDialogComponent.cancelAction(this.dialogRef);
  }

  public async swapRole() {
    this.loadingVisService.setIsLoading(true);
    // TODO: api message to swap role
    this.loadingVisService.setIsLoading(false);
    this.dialogRef.close(true);
  }
}
