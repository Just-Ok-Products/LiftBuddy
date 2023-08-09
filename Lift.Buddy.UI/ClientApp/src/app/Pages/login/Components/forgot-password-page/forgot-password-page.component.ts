import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { LoginService } from '../../Services/login.service';
import { LoginCredetials } from 'src/app/Model/LoginCredentials';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';

@Component({
  selector: 'app-forgot-password-page',
  templateUrl: './forgot-password-page.component.html',
  styleUrls: ['./forgot-password-page.component.css']
})
export class ForgotPasswordPageComponent implements OnInit {

  constructor(
    private loginService: LoginService,
    private snackbarService: SnackBarService
  ) { }

  ngOnInit() {
  }

  public passwordVisible: boolean = false;
  public confirmPasswordVisible: boolean = false;

  public username: FormControl = new FormControl('', Validators.required);
  public password: FormControl = new FormControl('', Validators.required);
  public confirmPassword: FormControl = new FormControl('', [Validators.required, this.notSamePasswordError]);

  public forgotPasswordForm: FormGroup = new FormGroup({
    username: this.username,
    password: this.password,
    confirmPassword: this.confirmPassword
  });

  public notSamePasswordError(g: FormControl): ValidationErrors {
    let confirmPassword = g.value;
    let password = g.parent?.value?.password;
    g.setErrors({})
    return confirmPassword == password ? {} : {noMatch: true};
  }

  public async changePassword() {
    let loginCredentials = new LoginCredetials();
    loginCredentials.password = this.password.value;
    loginCredentials.username = this.username.value;

    var response = await this.loginService.changePassword(loginCredentials)
    if (!response.result) {
      this.snackbarService.operErrorSnackbar(`Failed to change password. Error: ${response.notes}`);
    }
    this.snackbarService.openSuccessSnackbar('Password changed successfully');
  }

}
