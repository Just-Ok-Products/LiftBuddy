import { Injectable } from '@angular/core';
import { ApiCallsService } from './Utils/api-calls.service';
import { UserPersonalRecord } from '../Model/UserPersonalRecord';

@Injectable({
  providedIn: 'root'
})
export class PrService {

  constructor(
    private apiService: ApiCallsService
  ) { }

  private defaultUrl: string = "api/PersonalRecord";

  public get() {
    const response = this.apiService.apiGet<UserPersonalRecord>(this.defaultUrl);
    return response;
  }

  public savePR(userPR: UserPersonalRecord, isUpdate: boolean) {
    let response;
    if (isUpdate) {
      response = this.apiService.apiPut<UserPersonalRecord>(this.defaultUrl, userPR);
    } else {
      response = this.apiService.apiPost<UserPersonalRecord>(this.defaultUrl, userPR);
    }
    return response;
  }

}
