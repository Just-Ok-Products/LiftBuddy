import { Injectable } from '@angular/core';
import { ApiCallsService } from './Utils/api-calls.service';
import { PersonalRecord } from '../Model/PersonalRecord';

@Injectable({
    providedIn: 'root',
})

export class PersonalRecordService {

    private defaultUrl: string = 'api/PersonalRecord';

    constructor(private apiService: ApiCallsService) { }

    public get() {
        return this.apiService.apiGet<PersonalRecord>(this.defaultUrl);
    }

    public savePersonalRecord(personalRecords: { toUpdate: PersonalRecord[], toAdd: PersonalRecord[] }) {
        return this.apiService.apiPut<PersonalRecord[]>(this.defaultUrl, personalRecords);
    }
}
