import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { tap } from 'rxjs';
import { Exercise } from 'src/app/Model/Exercise';

@Component({
    selector: 'app-exercise-row',
    templateUrl: './exercise-row.component.html',
    styleUrls: ['./exercise-row.component.css']
})

export class ExerciseRowComponent implements OnInit {

    @Input() exercise: Exercise | undefined
    @Input() index: number | undefined
    @Output() onDelete: EventEmitter<any> = new EventEmitter();
    @Output() onChange: EventEmitter<any> = new EventEmitter();

    constructor() { }

    ngOnInit() {
        if (this.exercise) {
            this.initFormValues();
        }
    }

    private initFormValues() {
        this.exerciseForm.controls['name'].setValue(this.exercise?.name ?? '');
        this.exerciseForm.controls['reps'].setValue(this.exercise?.repetitions ?? 1);
        this.exerciseForm.controls['series'].setValue(this.exercise?.series ?? 1);
        this.exerciseForm.controls['rest'].setValue(this.exercise?.rest ?? 0);

        this.exerciseForm.controls['name'].valueChanges.pipe(
            tap((name: string) => this.exercise!.name = name)
        ).subscribe();

        this.exerciseForm.controls['reps'].valueChanges.pipe(
            tap((repetitions: number) => this.exercise!.repetitions = repetitions)
        ).subscribe();

        this.exerciseForm.controls['series'].valueChanges.pipe(
            tap((series: number) => this.exercise!.series = series)
        ).subscribe();

        this.exerciseForm.controls['rest'].valueChanges.pipe(
            tap((rest: Date) => this.exercise!.rest = rest)
        ).subscribe();
    }

    public exerciseForm: FormGroup = new FormGroup({
        name: new FormControl(''),
        reps: new FormControl(1),
        series: new FormControl(1),
        rest: new FormControl('01:00')
    });

    public updateExercise() {
        this.exercise!.name = this.exerciseForm.controls['name'].value;
        this.exercise!.repetitions = this.exerciseForm.controls['reps'].value;
        this.exercise!.series = this.exerciseForm.controls['series'].value;
        this.exercise!.rest = this.exerciseForm.controls['rest'].value;
    }

    public remove() {
        this.onDelete.emit(this.index);
    }
}
