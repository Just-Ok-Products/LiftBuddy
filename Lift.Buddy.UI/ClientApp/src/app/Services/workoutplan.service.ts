import { Injectable } from '@angular/core';
import { ApiCallsService } from './Utils/api-calls.service';
import { WorkoutSchedule } from '../Model/WorkoutSchedule';
import { BehaviorSubject } from 'rxjs';
import { SnackBarService } from './Utils/snack-bar.service';
import { LoginService } from './login.service';

@Injectable({
  providedIn: 'root'
})
export class WorkoutplanService {

constructor(
  private apiService: ApiCallsService,
  private loginService: LoginService,
  private snackbarService: SnackBarService
) { }

//#region Day observable
private _day: number | undefined
private day = new BehaviorSubject<number>(0);
public day$ = this.day.asObservable();

public getDay() {
  return this._day
}

public setDay(day: number) {
  this._day = day;
  this.day.next(day);
}
//#endregion

private defaultUrl: string = 'api/WorkoutSchedule';

public async getWorkoutPlanById(id: number) {
  const response = await this.apiService.apiGet<WorkoutSchedule>(this.defaultUrl + `/id/${id}`);
  return response;
}

public async getWorkoutPlanByUser() {
  const response = await this.apiService.apiGet<WorkoutSchedule>(this.defaultUrl + `/${this.loginService.currentUsername}`);
  return response;
}

public async saveWorkoutPlan(workout: WorkoutSchedule) {
  try {
    if (!workout) {
      throw new Error('No workout to save');
    }
    let saveResp;
    if (!workout.id) {
      saveResp = await this.apiService.apiPost('api/WorkoutSchedule', workout);
    } else {
      saveResp = await this.apiService.apiPut('api/WorkoutSchedule', workout);
    }
    if (!saveResp.result) {
      throw new Error(`${saveResp.notes}`);
    }
    this.snackbarService.openSuccessSnackbar('Workout saved')
  } catch (e) {
    this.snackbarService.operErrorSnackbar(`Failed to save workout schedule. Error: ${e}`);
  }
}

}
