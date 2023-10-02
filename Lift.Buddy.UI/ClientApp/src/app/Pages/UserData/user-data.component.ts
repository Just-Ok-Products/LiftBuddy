import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Model/User';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { LoginService } from 'src/app/Services/login.service';

@Component({
    selector: 'app-user-data',
    templateUrl: './user-data.component.html',
    styleUrls: ['./user-data.component.css']
})

export class UserDataComponent implements OnInit {

    public userData: User | undefined;

    constructor(
        private loginService: LoginService,
        private snackbarService: SnackBarService
    ) { }

    async ngOnInit() {
        await this.getUserData();
    }

    private async getUserData() {
        const response = await this.loginService.getUserData();
        if (!response.result) {
            this.snackbarService.operErrorSnackbar(`Not able to retrieve user data due to: ${response.notes}`)
        }

        this.userData = response.body[0];
    }

    public async save() {
        if (this.userData == undefined) {
            return;
        }

        const response = await this.loginService.updateUserData(this.userData);

        if (!response.result) {
            this.snackbarService.operErrorSnackbar(`Failed to update user data due to: ${response.notes}`)
            return;
        }

        this.snackbarService.openSuccessSnackbar('Succesfully updated user data')
    }

}
