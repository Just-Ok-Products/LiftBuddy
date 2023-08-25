import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Exercize } from 'src/app/Model/Exercise';
import { WorkoutDay } from 'src/app/Model/WorkoutDay';
import { WorkoutSchedule } from 'src/app/Model/WorkoutSchedule';

@Component({
  selector: 'app-daily-workout',
  templateUrl: './daily-workout.component.html',
  styleUrls: ['./daily-workout.component.css']
})
export class DailyWorkoutComponent implements OnInit {

  constructor() { }

  @Input() day: number = 0;
  @Input() workschedule: WorkoutSchedule | undefined;
  @Input() exercises: FormControl<Exercize[] | null> | undefined;
  @Output() onSave: EventEmitter<any> = new EventEmitter();

  ngOnInit() {
    this.initExercizes();
  }

  private initExercizes() {
    if (this.workschedule?.workoutDays[this.day]?.exercises?.length == 0) {
      return;
    }
  }

  public exerciseList: Exercize[] = [];
  public addExercise() {
    if (!this.exercises?.value) {
      let workout = new WorkoutDay();
      let exerciseList = [new Exercize()];
      workout.day = this.day;
      workout.exercises = exerciseList;
      this.workschedule?.workoutDays.push(workout);
      this.exercises?.setValue(exerciseList);
    } else {
      this.exercises?.value?.push(new Exercize());
    }
  }

  public save() {
    this.onSave.emit();
  }

  public remove(index: number) {
    this.exercises?.value?.splice(index, 1);

    if (this.exercises?.value?.length == 0) {
      const idx = this.workschedule?.workoutDays.findIndex(x => x.day == this.day);
      if (idx == -1) {
        return;
      }
      this.workschedule?.workoutDays.splice(idx!, 1);
    }
  }
}
