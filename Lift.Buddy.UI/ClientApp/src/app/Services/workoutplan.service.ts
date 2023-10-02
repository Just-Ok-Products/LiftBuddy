import { Injectable } from '@angular/core';
import { ApiCallsService } from './Utils/api-calls.service';
import { WorkoutPlan } from '../Model/WorkoutPlan';
import { BehaviorSubject } from 'rxjs';
import { SnackBarService } from './Utils/snack-bar.service';
import { LoginService } from './login.service';

@Injectable({
    providedIn: 'root'
})

export class WorkoutplanService {


    private defaultUrl: string = 'api/WorkoutPlan';
    private _day: number | undefined
    private day = new BehaviorSubject<number>(0);
    public day$ = this.day.asObservable();

    constructor(
        private apiService: ApiCallsService,
        private loginService: LoginService,
        private snackbarService: SnackBarService
    ) { }

    //#region Day observable
    public getDay() {
        return this._day
    }

    public setDay(day: number) {
        this._day = day;
        this.day.next(day);
    }
    //#endregion


    public async getWorkoutPlanById(id: number) {
        return await this.apiService.apiGet<WorkoutPlan>(this.defaultUrl + `/${id}`);
    }

    public async getWorkoutPlanByUser() {
        return await this.apiService.apiGet<WorkoutPlan>(this.defaultUrl);
    }

    public async getWorkoutPlansCreatedByUser(userId: string) {
        return await this.apiService.apiGet<WorkoutPlan>(this.defaultUrl + `/created-by/${userId}`);
    }

    public async getWorkoutPlanSubscribersCount(workoutPlan: WorkoutPlan) {
        return await this.apiService.apiGet<number>(this.defaultUrl + `/subscribers/${workoutPlan.id}`);
    }

    public async addWorkoutPlan(workout: WorkoutPlan) {
        try {
            if (!workout) {
                throw new Error('No workout to save');
            }
            console.log(workout)
            workout.createdBy = this.loginService.currentUsername;
            let response = await this.apiService.apiPost(this.defaultUrl + '/add', workout);

            if (!response.result) {
                throw new Error(`${response.notes}`);
            }

            this.snackbarService.openSuccessSnackbar('Workout saved')
        } catch (e) {
            this.snackbarService.operErrorSnackbar(`Failed to save workout schedule. Error: ${e}`);
        }
    }

    public async updateWorkoutPlan(workout: WorkoutPlan) {
        try {
            if (!workout) {
                throw new Error('No workout to save');
            }

            let response = await this.apiService.apiPut(this.defaultUrl, workout);

            if (!response.result) {
                throw new Error(`${response.notes}`);
            }

            this.snackbarService.openSuccessSnackbar('Workout saved')
        } catch (e) {
            this.snackbarService.operErrorSnackbar(`Failed to save workout schedule. Error: ${e}`);
        }
    }

    public async deleteWorkoutPlan(workoutId: string) {
        const response = await this.apiService.apiDelete(this.defaultUrl, { workoutId });
        return response;
    }
}
