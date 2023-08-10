import { Response } from './../../../Model/Response';
import { ApiCallsService } from '../../../Services/Utils/api-calls.service';
import { LoginCredetials } from '../../../Model/LoginCredentials';
import { Injectable } from '@angular/core';
import { RegistrationCredentials } from 'src/app/Model/RegistraitonCredentials';
import { SecurityQuestions } from 'src/app/Model/SecurityQuestions';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor (
    private apiCalls: ApiCallsService
  ) { }

  public async login(loginCredentials: LoginCredetials) {
    const response = await this.apiCalls.apiPost<string>("api/Login", loginCredentials);

    if (response.result) {
      this.apiCalls.jwtToken = response.body[0];
    }
    return response;
  }

  public registrationCredentials: RegistrationCredentials | undefined;
  public async register(registrationCredentials: RegistrationCredentials) {
    const response = await this.apiCalls.apiPost<RegistrationCredentials>('api/Login/register', registrationCredentials);
    return response;
  }

  public currentUsername: string = "";
  public async changePassword(loginCredential: LoginCredetials) {
    const response = await this.apiCalls.apiPost<LoginCredetials>('api/Login/changePassword', loginCredential);
    return response;
  }

  public async getSecurityQuestions(loginCredential: LoginCredetials) {
    const response = await this.apiCalls.apiPost<SecurityQuestions>(`api/Login/security-questions`, loginCredential);
    return response;
  }
}
