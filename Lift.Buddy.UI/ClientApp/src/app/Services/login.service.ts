import { Response } from '../Model/Response';
import { ApiCallsService } from './Utils/api-calls.service';
import { LoginCredetials } from '../Model/LoginCredentials';
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

  private defaultUrl: string = "api/Login";

  public async login(loginCredentials: LoginCredetials) {
    const response = await this.apiCalls.apiPost<string>(this.defaultUrl, loginCredentials);

    if (response.result) {
      this.apiCalls.jwtToken = response.body[0];
    }
    return response;
  }

  public registrationCredentials: RegistrationCredentials | undefined;
  public async register(registrationCredentials: RegistrationCredentials) {
    const response = await this.apiCalls.apiPost<RegistrationCredentials>(this.defaultUrl + '/register', registrationCredentials);
    return response;
  }

  public currentUsername: string = "";
  public async changePassword(loginCredential: LoginCredetials) {
    const response = await this.apiCalls.apiPost<LoginCredetials>(this.defaultUrl + '/changePassword', loginCredential);
    return response;
  }

  public async getSecurityQuestions(loginCredential: LoginCredetials) {
    const response = await this.apiCalls.apiPost<SecurityQuestions>(this.defaultUrl + `/security-questions`, loginCredential);
    return response;
  }
}
