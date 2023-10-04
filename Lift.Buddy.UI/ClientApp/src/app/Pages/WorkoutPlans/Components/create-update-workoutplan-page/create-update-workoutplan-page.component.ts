import { WorkoutplanService } from './../../../../Services/workoutplan.service';
import { WorkoutDay } from './../../../../Model/WorkoutDay';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Exercise } from 'src/app/Model/Exercise';
import { WorkoutPlan } from 'src/app/Model/WorkoutPlan';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';

@Component({
    selector: 'app-create-update-workoutplan-page',
    templateUrl: './create-update-workoutplan-page.component.html',
    styleUrls: ['./create-update-workoutplan-page.component.css']
})

export class CreateUpdateWorkoutplanPageComponent implements OnInit {

    @Input() isReadOnly: boolean = false

    public exercises = new FormControl<Exercise[]>([]);
    public workoutPlan: WorkoutPlan = new WorkoutPlan();
    public days: string[] = [];

    constructor(
        private activatedRoute: ActivatedRoute,
        private workoutPlanSerivice: WorkoutplanService,
        private snackbarService: SnackBarService
    ) { }

    async ngOnInit() {
        this.initDates();
        await this.initWorkschedule();
        this.initFormDataBinding();
    }

    public workoutDayForm: FormGroup = new FormGroup({
        name: new FormControl(),
        trainingDay: new FormControl(0),
        exercises: this.exercises
    });

    private initFormDataBinding() {
        this.workoutDayForm.controls['name'].valueChanges.subscribe(name => {
            this.workoutPlan.name = name;
        })

        this.workoutDayForm.controls['exercises'].valueChanges.subscribe(exercises => {
            const day = this.workoutDayForm.controls['trainingDay'].value;
            let workoutDay = this.workoutPlan.workoutDays.find(x => x.day == day);
            if (workoutDay == null) {
                workoutDay = new WorkoutDay();
                workoutDay.day = day;
            }

            workoutDay.exercises = exercises;
        })

        this.workoutDayForm.controls['trainingDay'].valueChanges.subscribe(day => {
            const workoutDay = this.workoutPlan.workoutDays.find(x => x.day == day);
            let exercises;
            if (workoutDay == undefined) {
                let newWorkoutDay = new WorkoutDay();
                newWorkoutDay.day = day;

                this.workoutPlan.workoutDays.push(newWorkoutDay)
                exercises = newWorkoutDay.exercises;
            } else {
                exercises = workoutDay.exercises;
            }

            this.workoutDayForm.controls['exercises'].setValue(exercises);
        })
    }

    //#region Init Dates

    /** Days of the week */
    private initDates() {
        const locale = navigator.language; // TODO: make app localizable
        for (let i = 0; i < 7; i++) {
            const element = new Date(1990, 0, i).toLocaleDateString('en-US', { weekday: 'long' });
            this.days?.push(element);
        }
    }

    //#endregion

    private async initWorkschedule() {
        const workoutId = this.activatedRoute.snapshot.paramMap.get('workoutId')!;
        if (workoutId == "new") {
            this.workoutPlan = new WorkoutPlan();
            return;
        }

        const response = await this.workoutPlanSerivice.getWorkoutPlanById(workoutId);
        if (!response.result || response.body.length == 0) {
            this.snackbarService.operErrorSnackbar("Failed to load workout plan");
            return;
        }

        this.workoutPlan = response.body[0];
        this.workoutPlan.workoutDays.sort((x: any, y: any) => x.day! - y.day!);

        if (this.workoutPlan.workoutDays.length != 0) {
            const day: number = this.workoutPlan.workoutDays[0].day!;
            const exercises = this.workoutPlan.workoutDays.find((x: any) => x.day == day)?.exercises;
            this.workoutDayForm.controls['trainingDay'].setValue(day);
            this.workoutDayForm.controls['exercises'].setValue(exercises);
        }

        this.workoutDayForm.controls['name'].setValue(this.workoutPlan.name);
    }

    public async save() {
        this.deleteEmptyDays();

        if (this.workoutPlan.id == "")
            this.workoutPlanSerivice.addWorkoutPlan(this.workoutPlan);
        else
            this.workoutPlanSerivice.updateWorkoutPlan(this.workoutPlan);
    }

    /** Deletes empty days from object to save space on DB */
    private deleteEmptyDays() {
        this.workoutPlan.workoutDays.forEach((x, idx) => {
            if (x.exercises.length == 0) {
                this.workoutPlan.workoutDays.splice(idx, 1);
            }
        })
    }

}
