import { FormGroup, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/Services/login.service';
import { LoginCredetials } from 'src/app/Model/LoginCredentials';
import { SnackBarService } from 'src/app/Services/Utils/snack.bar.service';

@Component({
  selector: 'app-login',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css', '../../../../app.component.scss']
})
export class LoginPageComponent implements OnInit {

  constructor(
    private loginService: LoginService,
    private snackBarService: SnackBarService
  ) { }

  public forgotPasswordVisible: boolean = false;
  public passwordVisible: boolean = false;

  public username = new FormControl();
  public password = new FormControl();

  public loginForm = new FormGroup({
    username: this.username,
    password: this.password
  })

  ngOnInit() {
  }

  public async login() {
    let loginCredentials = new LoginCredetials();

    loginCredentials.password = this.loginForm.value.password;
    loginCredentials.username = this.loginForm.value.username;

    var response = await this.loginService.login(loginCredentials);

    if (!response.result) {
      this.snackBarService.openSnackbar("Login failed")
      this.forgotPasswordVisible = true;
    } else {
      this.snackBarService.openSnackbar("Login successfull")
      console.log(response.body)
    }
  }

}
