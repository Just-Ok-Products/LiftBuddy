import { Component, OnInit } from '@angular/core';
import { COMMA, ENTER, SPACE } from '@angular/cdk/keycodes';
import { MatChipInputEvent } from '@angular/material/chips';
import { WorkoutPlan } from 'src/app/Model/WorkoutPlan';
import { WorkoutplanService } from 'src/app/Services/workoutplan.service';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { DialogService } from 'src/app/Services/Utils/dialog.service';
import { DeleteWorkoutPlanConfirmationPopupComponent } from './Components/delete-workout-plan-confirmation-popup/delete-workout-plan-confirmation-popup.component';
import { LoginService } from 'src/app/Services/login.service';

@Component({
    selector: 'app-my-workouts',
    templateUrl: './my-workouts.component.html',
    styleUrls: ['./my-workouts.component.css']
})

export class MyWorkoutsComponent implements OnInit {

    public separatorKeyCodes = [COMMA, ENTER, SPACE];
    public workouts: WorkoutPlan[] = [];
    public items: string[] = [];
    public ratings: number[] = [2, 5];
    public workoutPlanSubscribers: number[] = [];

    constructor(
        private loginService: LoginService,
        private workoutplanService: WorkoutplanService,
        private snackbarService: SnackBarService,
        private dialogService: DialogService
    ) { }

    async ngOnInit() {
        await this.initWorkouts();
    }

    private async initWorkouts() {
        const workoutPlanResp = await this.workoutplanService.getWorkoutPlansCreatedByUser(this.loginService.user?.id!);
        if (!workoutPlanResp.result) {
            this.snackbarService.operErrorSnackbar(`Failed ot load workouts due to: ${workoutPlanResp.notes}`)
        }
        this.workouts = workoutPlanResp.body;
        this.workouts.forEach(async workoutPlan => {
            const response = await this.workoutplanService.getWorkoutPlanSubscribersCount(workoutPlan);
            if (!response.result) {
                this.snackbarService.operErrorSnackbar(`Failed to load number of people subscribed to ${workoutPlan.name}. Error ${response.notes}`)
            }
            const subscribersQuantity = response.body.pop() ?? 0;
            this.workoutPlanSubscribers.push(subscribersQuantity);
        });
    }

    public add(event: MatChipInputEvent) {
        const value = (event.value || '').trim();

        if (value) {
            this.items.push(value);
        }

        event.chipInput!.clear();
    }

    public remove(item: any) {
        const idx = this.items.indexOf(item);

        if (idx >= 0) {
            this.items.splice(idx, 1);
        }
    }

    public async deleteWorkout(workout: WorkoutPlan) {
        const dialogRef = this.dialogService.openCenterDialog(
            DeleteWorkoutPlanConfirmationPopupComponent,
            {
                data: { workout: workout }
            }
        );

        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                const deletedWorkoutIdx = this.workouts.indexOf(workout);
                if (deletedWorkoutIdx != -1) {
                    this.workouts.splice(deletedWorkoutIdx, 1)
                }
            }
        })
    }
}
