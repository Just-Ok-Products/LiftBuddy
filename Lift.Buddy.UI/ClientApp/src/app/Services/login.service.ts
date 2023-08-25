import { ApiCallsService } from './Utils/api-calls.service';
import { LoginCredetials } from '../Model/LoginCredentials';
import { Injectable, inject } from '@angular/core';
import { RegistrationCredentials } from 'src/app/Model/RegistraitonCredentials';
import { SecurityQuestions } from 'src/app/Model/SecurityQuestions';
import { CanActivateFn, Router } from '@angular/router';
import { SnackBarService } from './Utils/snack-bar.service';
import { UserData } from '../Model/UserData';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor (
    private apiCalls: ApiCallsService
  ) { }

  private defaultUrl: string = "api/Login";

  public async getUserData() {
    let userData = new UserData();
    userData.username = this.currentUsername;
    const response = await this.apiCalls.apiPost<UserData>(this.defaultUrl + '/user-data', userData);

    return response;
  }

  public async updateUserData(userData: UserData) {
    const response = await this.apiCalls.apiPut<UserData>(this.defaultUrl + '/user-data', userData);

    return response;
  }

  public async login(loginCredentials: LoginCredetials) {
    const response = await this.apiCalls.apiPost<string>(this.defaultUrl, loginCredentials);

    if (response.result) {
      ApiCallsService.jwtToken = response.body[0];
    }
    return response;
  }

  public registrationCredentials: RegistrationCredentials | undefined;
  public async register(registrationCredentials: RegistrationCredentials) {
    const response = await this.apiCalls.apiPost<RegistrationCredentials>(this.defaultUrl + '/register', registrationCredentials);
    return response;
  }

  public logout() {
    this.currentUsername = '';
    ApiCallsService.jwtToken = '';
    return true;
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

  public static isLoggedInGuard(): CanActivateFn {
    return () => {
      if (ApiCallsService.jwtToken != undefined) {
        return true;
      }
      const router: Router = inject(Router);
      const snackbarService: SnackBarService = inject(SnackBarService);
      snackbarService.operErrorSnackbar('You have to login first')
      router.navigate(['login'])
      return false;
    }
  }
}
