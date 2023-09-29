import { AuthGuard } from './../../../../Services/Guards/AuthGuard';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/Services/login.service';
import { Credentials } from 'src/app/Model/Credentials';
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
    private router: Router,
    private authGuard: AuthGuard
  ) { }

  public forgotPasswordVisible: boolean = false;
  public passwordVisible: boolean = false;

  public loginForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  })

  ngOnInit() {
  }

  public async login() {

    if (!this.loginForm.valid) {
      this.snackBarService.operErrorSnackbar("Fill the required fields.");
      return;
    }
    
    let loginCredentials = new Credentials(this.loginForm.controls['username'].value!, this.loginForm.controls['password'].value!);

    var response = await this.loginService.login(loginCredentials);

    if (!response.result) {
      this.snackBarService.operErrorSnackbar("Login failed")
      this.forgotPasswordVisible = true;
    } else {
      this.snackBarService.openSuccessSnackbar("Login successfull")
      this.loginService.currentUsername = this.loginForm.controls['username'].value!;
      const previousPage = this.authGuard.previousPage?.initialUrl ?? '';
      this.router.navigateByUrl(previousPage);
    }
  }

  public goToForgotPasswordPage() {
    this.forgotPasswordVisible = !this.forgotPasswordVisible;
    this.loginService.currentUsername = this.loginForm.controls['username'].value!;

    this.router.navigate(['login', 'forgot-password']);
  }
}
