import { Component, OnInit } from '@angular/core';
import { UserData } from 'src/app/Model/UserData';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { LoginService } from 'src/app/Services/login.service';

@Component({
  selector: 'app-user-data',
  templateUrl: './user-data.component.html',
  styleUrls: ['./user-data.component.css']
})
export class UserDataComponent implements OnInit {

  constructor(
    private loginService: LoginService,
    private snackbarService: SnackBarService
  ) { }

  async ngOnInit() {
    await this.getUserData();
  }

  public userData: UserData | undefined;

  private async getUserData() {
    let userDataResp = await this.loginService.getUserData();

    if (!userDataResp.result) {
      this.snackbarService.operErrorSnackbar(`Not able to retrieve user data due to: ${userDataResp.notes}`)
    }

    this.userData = userDataResp.body[0];
    this.snackbarService.openSuccessSnackbar('User data successfully retrieved');
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
