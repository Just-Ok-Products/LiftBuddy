import { ApiCallsService } from '../../../Services/Utils/api-calls.service';
import { LoginCredetials } from '../../../Model/LoginCredentials';

import { Injectable } from '@angular/core';
import { Response } from '../../../Model/Response';
import { RegistrationCredentials } from 'src/app/Model/RegistraitonCredentials';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor (
    private apiCalls: ApiCallsService
  ) { }

  public async login(loginCredentials: LoginCredetials): Promise<Response> {
    const response = await this.apiCalls.apiPost("api/Login", loginCredentials);

    if (response.result) {
      this.apiCalls.jwtToken = response.body;
    }
    return response;
  }

  public registrationCredentials: RegistrationCredentials | undefined;
  public async register(registrationCredentials: RegistrationCredentials): Promise<Response> {
    const response = await this.apiCalls.apiPost('api/Login/register', registrationCredentials);
    return response;
  }

  public async changePassword(loginCredential: LoginCredetials) {
    const response = await this.apiCalls.apiPost('api/Login/changePassword', loginCredential);
    return response;
  }
}
