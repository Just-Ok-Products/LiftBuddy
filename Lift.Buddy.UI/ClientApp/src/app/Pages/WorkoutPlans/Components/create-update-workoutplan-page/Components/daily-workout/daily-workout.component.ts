import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Exercise } from 'src/app/Model/Exercise';
import { WorkoutDay } from 'src/app/Model/WorkoutDay';
import { WorkoutPlan } from 'src/app/Model/WorkoutPlan';

@Component({
  selector: 'app-daily-workout',
  templateUrl: './daily-workout.component.html',
  styleUrls: ['./daily-workout.component.css']
})
export class DailyWorkoutComponent implements OnInit {

  constructor() { }

  @Input() day: number = 0;
  @Input() workoutPlan: WorkoutPlan | undefined;
  @Input() exercises: FormControl<Exercise[] | null> | undefined;
  @Output() onSave: EventEmitter<any> = new EventEmitter();

  ngOnInit() {
    this.initexercises();
  }

  private initexercises() {
    if (this.workoutPlan?.workoutDays[this.day]?.exercises?.length == 0) {
      return;
    }
  }

  public exerciseList: Exercise[] = [];
  public addExercise() {
    if (!this.exercises?.value) {
      let workout = new WorkoutDay();
      let exerciseList = [new Exercise()];
      workout.day = this.day;
      workout.exercises = exerciseList;
      this.workoutPlan?.workoutDays.push(workout);
      this.exercises?.setValue(exerciseList);
    } else {
      this.exercises?.value?.push(new Exercise());
    }
  }

  public save() {
    this.onSave.emit();
  }

  public remove(index: number) {
    this.exercises?.value?.splice(index, 1);

    if (this.exercises?.value?.length == 0) {
      const idx = this.workoutPlan?.workoutDays.findIndex(x => x.day == this.day);
      if (idx == -1) {
        return;
      }
      this.workoutPlan?.workoutDays.splice(idx!, 1);
    }
  }
}
