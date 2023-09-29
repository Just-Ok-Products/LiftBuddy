import { ApiCallsService } from './Utils/api-calls.service';
import { Injectable, inject } from '@angular/core';
import { Credentials } from 'src/app/Model/Credentials';
import { CanActivateFn, Router } from '@angular/router';
import { SnackBarService } from './Utils/snack-bar.service';
import { User } from '../Model/User';
import { SecurityQuestions } from '../Model/SecurityQuestions';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor (private apiCalls: ApiCallsService) { }

  private defaultUrl: string = "api/Auth";
  public currentUsername: string = "";
  public user : User | undefined;

  public async getUserData() {
    const response = await this.apiCalls.apiGet<User>(this.defaultUrl + '/user-data');

    return response;
  }

  public async updateUserData(userData: User) {
    const response = await this.apiCalls.apiPut<User>(this.defaultUrl + '/user-data', userData);

    return response;
  }

  public async login(credentials: Credentials) {
    const response = await this.apiCalls.apiPost<string>(this.defaultUrl + '/login', credentials);

    if (response.result) {
      ApiCallsService.jwtToken = response.body[0];
    }

    return response;
  }

  public async register(user: User) {
    const response = await this.apiCalls.apiPost<User>(this.defaultUrl + '/register', user);
    return response;
  }

  public logout() {
    ApiCallsService.jwtToken = '';
    return true;
  }

  public async changePassword(credential: Credentials) {
    const response = await this.apiCalls.apiPost<Credentials>(this.defaultUrl + '/changePassword', credential);
    return response;
  }

  public async getSecurityQuestions(username: Credentials) {
    const response = await this.apiCalls.apiPost<SecurityQuestions>(this.defaultUrl + `/security-questions`, username);
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
