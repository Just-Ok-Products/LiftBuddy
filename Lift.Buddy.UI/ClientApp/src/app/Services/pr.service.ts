import { Injectable } from '@angular/core';
import { ApiCallsService } from './Utils/api-calls.service';
import { UserPR } from '../Model/UserPR';

@Injectable({
  providedIn: 'root'
})
export class PrService {

  constructor(
    private apiService: ApiCallsService
  ) { }

  public get(username: string) {
    const response = this.apiService.apiGet<UserPR>(`api/PR/${username}`);
    return response;
  }

  public savePR(userPR: UserPR, isUpdate: boolean) {
    let response;
    if (isUpdate) {
      response = this.apiService.apiPut<UserPR>('api/PR', userPR);
    } else {
      response = this.apiService.apiPost<UserPR>('api/PR', userPR);
    }
    return response;
  }

}
