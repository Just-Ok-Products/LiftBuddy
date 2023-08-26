import { WorkoutDay } from 'src/app/Model/WorkoutDay';
import { Component, OnInit } from '@angular/core';
import { WorkoutSchedule } from 'src/app/Model/WorkoutSchedule';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { WorkoutplanService } from 'src/app/Services/workoutplan.service';

@Component({
  selector: 'app-your-workouts-page',
  templateUrl: './your-workouts-page.component.html',
  styleUrls: ['./your-workouts-page.component.css']
})
export class YourWorkoutsPageComponent implements OnInit {

  constructor(
    private workoutplanService: WorkoutplanService,
    private snackbarService: SnackBarService,
  ) { }

  async ngOnInit() {
    await this.initUserWorkouts()
  }

  public workouts: WorkoutSchedule[] | undefined;
  public dailyWorkouts: {workout: WorkoutDay, name: string}[]  = []

  private async initUserWorkouts() {
    const workoutsResp = await this.workoutplanService.getWorkoutPlanByUser();

    if (!workoutsResp.result) {
      this.snackbarService.operErrorSnackbar(`Failed to load workouts due to: ${workoutsResp.notes}`)
    }

    this.workouts = workoutsResp.body;

    const dayNumber = new Date().getDay();

    workoutsResp.body.forEach(workout => {
      const workoutDay = workout.workoutDays.find(x => x.day == dayNumber);
      if (workoutDay != undefined) {
        this.dailyWorkouts.push({workout: workoutDay, name: workout.name});
      }
    });

  }

}
