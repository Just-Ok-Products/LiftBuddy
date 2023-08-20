import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/Services/login.service';
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

  public loginForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('')
  })

  ngOnInit() {
  }

  public async login() {

    if (!this.loginForm.valid) {
      this.snackBarService.operErrorSnackbar("Fill the required fields.");
      return;
    }
    let loginCredentials = new LoginCredetials();

    loginCredentials.password = this.loginForm.controls['password'].value;
    loginCredentials.username = this.loginForm.controls['username'].value;

    var response = await this.loginService.login(loginCredentials);

    if (!response.result) {
      this.snackBarService.operErrorSnackbar("Login failed")
      this.forgotPasswordVisible = true;
    } else {
      this.snackBarService.openSuccessSnackbar("Login successfull")
      this.loginService.currentUsername = this.loginForm.controls['username'].value!;
      this.router.navigate(['home']);
    }
  }

  public goToForgotPasswordPage() {
    this.forgotPasswordVisible = !this.forgotPasswordVisible;
    this.loginService.currentUsername = this.loginForm.controls['username'].value!;

    this.router.navigate(['login', 'forgot-password']);
  }

}
