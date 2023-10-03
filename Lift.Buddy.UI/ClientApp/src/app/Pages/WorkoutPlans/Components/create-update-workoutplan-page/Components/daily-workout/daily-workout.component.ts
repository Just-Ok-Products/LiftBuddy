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

    @Input() day: number = 0;
    @Input() workoutPlan: WorkoutPlan | undefined;
    @Input() exercises: FormControl<Exercise[] | null> | undefined;
    @Output() onSave: EventEmitter<any> = new EventEmitter();

    public exerciseList: Exercise[] = [];

    constructor() { }

    ngOnInit() {
        this.initExercises();
    }

    private initExercises() {
        if (this.workoutPlan?.workoutDays[this.day]?.exercises?.length == 0) {
            return;
        }
    }

    public addExercise() {
        if (!this.exercises?.value) {
            var exercises = [new Exercise()]

            const workoutDay: WorkoutDay = {
                day: this.day,
                exercises: exercises
            }

            this.workoutPlan?.workoutDays.push(workoutDay);
            this.exercises?.setValue(exercises);
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
