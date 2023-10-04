import { DialogRef } from '@angular/cdk/dialog';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-confirmation-dialog',
  templateUrl: './confirmation-dialog.component.html',
  styleUrls: ['./confirmation-dialog.component.css']
})
export class ConfirmationDialogComponent {

  @Input() public action: string = '';
  @Input() public confirmButtonName: string = '';
  @Input() public cancelButtonName: string = '';
  @Output() public confirmFunction: EventEmitter<void> = new EventEmitter();
  @Output() public cancelFunction: EventEmitter<void> = new EventEmitter();

  public static cancelAction(dialogRef: MatDialogRef<any, any>) {
    dialogRef.close(false);
  }

}
