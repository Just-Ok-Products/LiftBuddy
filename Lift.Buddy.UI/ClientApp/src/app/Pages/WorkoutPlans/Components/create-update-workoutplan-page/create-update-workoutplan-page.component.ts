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
            let workschedDay = this.workoutPlan.workoutDays.find(x => x.day == day);
            if (workschedDay == null) {
                workschedDay = new WorkoutDay();
                workschedDay.day = day;
            }
            workschedDay.exercises = exercises;
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
        const workoutId = +(this.activatedRoute.snapshot.paramMap.get('workoutId') ?? -1);
        if (workoutId < 0) {
            this.workoutPlan = new WorkoutPlan();
            return;
        }

        const workplanResp = await this.workoutPlanSerivice.getWorkoutPlanById(workoutId);
        if (!workplanResp.result || workplanResp.body.length == 0) {
            this.snackbarService.operErrorSnackbar("Failed to load workout plan");
            return;
        }

        this.workoutPlan = workplanResp.body[0];
        this.workoutPlan.workoutDays.sort((x: any, y: any) => x.day! - y.day!);

        const day: number = this.workoutPlan.workoutDays[0].day!;
        const exercises = this.workoutPlan.workoutDays.find((x: any) => x.day == day)?.exercises;

        this.workoutDayForm.controls['name'].setValue(this.workoutPlan.name);
        this.workoutDayForm.controls['trainingDay'].setValue(day);
        this.workoutDayForm.controls['exercises'].setValue(exercises);
    }

    public async save() {
        this.deleteEmptyDays();
        this.workoutPlanSerivice.addWorkoutPlan(this.workoutPlan);
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
