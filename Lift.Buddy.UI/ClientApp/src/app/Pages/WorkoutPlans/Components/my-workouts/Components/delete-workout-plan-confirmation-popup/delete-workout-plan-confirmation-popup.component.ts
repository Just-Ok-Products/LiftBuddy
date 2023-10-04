import { Component, Inject, Input, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { concatWith } from 'rxjs';
import { WorkoutPlan } from 'src/app/Model/WorkoutPlan';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { WorkoutplanService } from 'src/app/Services/workoutplan.service';

@Component({
    selector: 'app-delete-workout-plan-confirmation-popup',
    templateUrl: './delete-workout-plan-confirmation-popup.component.html',
    styleUrls: ['./delete-workout-plan-confirmation-popup.component.css']
})

export class DeleteWorkoutPlanConfirmationPopupComponent implements OnInit {

    public workout: WorkoutPlan = new WorkoutPlan();

    constructor(
        private workoutplanService: WorkoutplanService,
        private snackbarService: SnackBarService,
        private dialogRef: MatDialogRef<DeleteWorkoutPlanConfirmationPopupComponent>,
        @Inject(MAT_DIALOG_DATA) private data: { workout: WorkoutPlan }
    ) { }

    ngOnInit() {
        this.workout = this.data.workout;
    }

    public async delete() {
        const response = await this.workoutplanService.deleteWorkoutPlan(this.workout.id);
        if (!response.result) {
            this.snackbarService.operErrorSnackbar(`Failed to delete workoutplan ${this.workout.name}`);
        }
        this.dialogRef.close(true);
    }

    public cancel() {
        this.dialogRef.close(false);
    }

}
