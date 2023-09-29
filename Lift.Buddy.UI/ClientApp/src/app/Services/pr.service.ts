import { Injectable } from '@angular/core';
import { ApiCallsService } from './Utils/api-calls.service';
import { PersonalRecord } from '../Model/PersonalRecord';

@Injectable({
  providedIn: 'root'
})

export class PersonalRecordService {

  constructor(
    private apiService: ApiCallsService
  ) { }

  private defaultUrl: string = "api/PersonalRecord";

  public get() {
    const response = this.apiService.apiGet<PersonalRecord>(this.defaultUrl);
    return response;
  }

  public addPersonalRecord(username: string, personalRecords: PersonalRecord[]) {
    return this.apiService.apiPost<PersonalRecord>(this.defaultUrl, { username, personalRecords});
}

  public updatePersonalRecord(username: string, personalRecords: PersonalRecord[]) {
      return this.apiService.apiPut<PersonalRecord>(this.defaultUrl, { username, personalRecords});
  }

}
