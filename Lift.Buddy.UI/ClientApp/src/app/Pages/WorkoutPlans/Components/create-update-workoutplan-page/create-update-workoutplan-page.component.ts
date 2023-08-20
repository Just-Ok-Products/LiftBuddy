import { WorkoutplanService } from './../../../../Services/workoutplan.service';
import { WorkoutDay } from './../../../../Model/WorkoutDay';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Exercize } from 'src/app/Model/Exercise';
import { WorkoutSchedule } from 'src/app/Model/WorkoutSchedule';
import { ApiCallsService } from 'src/app/Services/Utils/api-calls.service';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';

@Component({
  selector: 'app-create-update-workoutplan-page',
  templateUrl: './create-update-workoutplan-page.component.html',
  styleUrls: ['./create-update-workoutplan-page.component.css']
})
export class CreateUpdateWorkoutplanPageComponent implements OnInit {

  constructor(
    private apiSevice: ApiCallsService,
    private activatedRoute: ActivatedRoute,
    private workoutPlanSerivice: WorkoutplanService,
    private snackbarService: SnackBarService
  ) { }

   async ngOnInit() {
    this.initDates();
    await this.initWorkschedule();
  }

  /** Days of the week */
  public days: string[] = [];
  private initDates() {
    const locale = navigator.language; // TODO: make app localizable
    for (let i = 0; i < 7; i++) {
      const element = new Date(1990, 0, i).toLocaleDateString('en-US', {weekday: 'long'});
      this.days?.push(element);
    }
  }

  public workschedule: WorkoutSchedule | undefined;
  private async initWorkschedule() {
    try {
      const workoutId = +(this.activatedRoute.snapshot.paramMap.get('workoutId') ?? -1);
      if (workoutId < 0) {
        this.workschedule = new WorkoutSchedule();
        return;
      }
      const workplanResp = await this.workoutPlanSerivice.getWorkoutPlan(workoutId);
      if (!workplanResp.result) {
        this.snackbarService.operErrorSnackbar("Failed to load workout plan");
        return;
      }

      if (workplanResp.body.length == 0) {
        this.workschedule = new WorkoutSchedule();
      } else {
        this.workschedule = workplanResp.body[0];
        const day: number = workplanResp.body[0].workoutDays[0].day!;
        const exercises = this.workschedule?.workoutDays.find(x => x.day == day)?.exercises;
        this.workoutDayForm.controls['trainingDay'].setValue(day);
        this.workoutDayForm.controls['exercises'].setValue(exercises);
      }
    } catch (ex) {
    }

  }

  public exercises = new FormControl<Exercize[]>([]);
  public workoutDayForm: FormGroup = new FormGroup({
    trainingDay: new FormControl(0),
    exercises: this.exercises
  });

  public onDayChange(day: number) {
    const exercises = this.workschedule?.workoutDays.find(x => x.day == day)?.exercises;
    this.workoutDayForm.controls['exercises'].setValue(exercises);
  }


  public async save() {
    this.workoutPlanSerivice.saveWorkoutPlan(this.workschedule!);
  }

}
