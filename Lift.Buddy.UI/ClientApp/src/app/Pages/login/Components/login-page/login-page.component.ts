import { FormGroup, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/Pages/login/Services/login.service';
import { LoginCredetials } from 'src/app/Model/LoginCredentials';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {

  constructor(
    private loginService: LoginService,
    private snackBarService: SnackBarService,
    private router: Router
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
      this.snackBarService.operErrorSnackbar("Login failed")
      this.forgotPasswordVisible = true;
    } else {
      this.snackBarService.openSuccessSnackbar("Login successfull")
      console.log(response.body)
    }
  }

  public goToForgotPasswordPage() {
    this.forgotPasswordVisible = !this.forgotPasswordVisible;
    this.loginService.currentUsername = this.loginForm.value.username;

    this.router.navigate(['login', 'forgot-password']);
  }

}
