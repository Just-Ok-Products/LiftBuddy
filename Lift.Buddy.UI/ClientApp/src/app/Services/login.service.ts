import { ApiCallsService } from './Utils/api-calls.service';
import { Injectable, inject } from '@angular/core';
import { Credentials } from 'src/app/Model/Credentials';
import { CanActivateFn, Router } from '@angular/router';
import { SnackBarService } from './Utils/snack-bar.service';
import { User } from '../Model/User';
import { SecurityQuestion } from '../Model/SecurityQuestions';

@Injectable({
    providedIn: 'root'
})

export class LoginService {

    private defaultUrl: string = "api/Auth";
    public currentUsername: string = "";
    public user: User | undefined;
    public isTrainer: boolean = false // TODO: assign a value from data
    // usare subject per condividere il valore, non tenerlo nel login service e passarlo in giro
    public userId: string = "";

    constructor(private apiCalls: ApiCallsService) { }

    public async getUserData() {
        return await this.apiCalls.apiGet<User>(this.defaultUrl + '/user-data');
    }

    public async updateUserData(userData: User) {
        return await this.apiCalls.apiPut<User>(this.defaultUrl + '/user-data', userData);
    }

    public async login(credentials: Credentials) {
        const response = await this.apiCalls.apiPost<string>(this.defaultUrl, credentials);

        if (response.result) {
            ApiCallsService.jwtToken = response.body[0];
            this.userId = response.body[1];
        }
        return response;
    }

    public async register(user: User) {
        return await this.apiCalls.apiPost<User>(this.defaultUrl + '/register', user);
    }

    public logout() {
        ApiCallsService.jwtToken = '';
        return true;
    }

    public async changePassword(credential: Credentials) {
        return await this.apiCalls.apiPost<Credentials>(this.defaultUrl + '/change-password', credential);
    }

    public async getSecurityQuestions() {
        return await this.apiCalls.apiGet<SecurityQuestion>(this.defaultUrl + `/security-questions`);
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
