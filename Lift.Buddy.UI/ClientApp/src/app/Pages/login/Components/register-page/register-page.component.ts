import { Component, OnInit } from '@angular/core';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { LoginService } from '../../../../Services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent implements OnInit {

  constructor(
    private snackbarService: SnackBarService,
    private loginService: LoginService,
    private router: Router
  ) { }

  ngOnInit() { }

  public registrationStep: number = 0;

  public getUserData() {
    if (this.loginService.user?.credentials == undefined) {
      this.snackbarService.operErrorSnackbar("Compile the form properly");
    } else {
      this.registrationStep += 1;
    }
  }

  public back() {
    this.registrationStep -= 1;
  }

  public async register() {
    try {
      let response = await this.loginService.register(this.loginService.user!);
      if (!response.result) {
        throw new Error("Failed to register " + response.notes);
      }
      this.snackbarService.openSuccessSnackbar("Registration complete");
      this.router.navigate(['/', 'login']);
    } catch (error) {
      this.snackbarService.operErrorSnackbar(error as string);
    }
  }

}
