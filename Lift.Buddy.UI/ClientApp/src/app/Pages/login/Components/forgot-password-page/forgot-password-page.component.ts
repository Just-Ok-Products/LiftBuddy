import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { LoginService } from '../../../../Services/login.service';
import { Credentials } from 'src/app/Model/Credentials';
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

  ngOnInit() { }

  public passwordVisible: boolean = false;
  public confirmPasswordVisible: boolean = false;
  public changePasswordVisibilityFlag: boolean = false;
  
  public forgotPasswordForm: FormGroup = new FormGroup({
    response: new FormControl('', Validators.required),
  });
  
  public changePasswordForm: FormGroup = new FormGroup({
    password: new FormControl('', Validators.required),
    confirmPassword: new FormControl('', [Validators.required, this.notSamePasswordError])
  });

  public notSamePasswordError(g: FormControl): ValidationErrors {
    let confirmPassword = g.value;
    let password = g.parent?.value?.password;
    g.setErrors({})
    return confirmPassword == password ? {} : {noMatch: true};
  }

  public async changePassword() {
    let loginCredentials = new Credentials(this.loginService.currentUsername, this.changePasswordForm.controls['password'].value);

    var response = await this.loginService.changePassword(loginCredentials)
    if (!response.result) {
      this.snackbarService.operErrorSnackbar(`Failed to change password. Error: ${response.notes}`);
    }

    this.snackbarService.openSuccessSnackbar('Password changed successfully');
  }
}
