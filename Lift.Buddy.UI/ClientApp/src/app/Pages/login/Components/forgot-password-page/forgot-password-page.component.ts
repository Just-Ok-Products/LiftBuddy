import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { LoginService } from '../../Services/login.service';
import { LoginCredetials } from 'src/app/Model/LoginCredentials';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { SecurityQuestions } from 'src/app/Model/SecurityQuestions';

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
    this.initQuesitons()
  }

  public questions: string[] = [];
  public answers: string[] = [];
  private async initQuesitons() {
    let loginCredentials: LoginCredetials = new LoginCredetials();
    loginCredentials.username = this.loginService.currentUsername;
    let secQuestionsResp = await this.loginService.getSecurityQuestions(loginCredentials);
    if (!secQuestionsResp.result) {
      this.snackbarService.operErrorSnackbar(`Unable to get security questions. Error: ${secQuestionsResp.notes}`);
    }
    if (secQuestionsResp.body.length == 0) {
      this.snackbarService.operErrorSnackbar(`There are no security questions.`);
      return;
    }
    let securityQuestions = secQuestionsResp.body[0];
    this.questions = securityQuestions.questions ?? [];
    this.answers = securityQuestions.answers ?? [];
  }

  public passwordVisible: boolean = false;
  public confirmPasswordVisible: boolean = false;

  public forgotPasswordForm: FormGroup = new FormGroup({
    response: new FormControl('', Validators.required),
  });

  public notSamePasswordError(g: FormControl): ValidationErrors {
    let confirmPassword = g.value;
    let password = g.parent?.value?.password;
    g.setErrors({})
    return confirmPassword == password ? {} : {noMatch: true};
  }

  public changePasswordVisibilityFlag: boolean = false;
  public answer() {
    const reply = this.forgotPasswordForm.controls['response'].value;
    if (reply == this.answers[0]) {
      this.changePasswordVisibilityFlag = true;
    }
  }

  public changePasswordForm: FormGroup = new FormGroup({
    password: new FormControl('', Validators.required),
    confirmPassword: new FormControl('', [Validators.required, this.notSamePasswordError])
  });

  public async changePassword() {
    let loginCredentials = new LoginCredetials();
    loginCredentials.username = this.loginService.currentUsername;
    loginCredentials.password = this.changePasswordForm.controls['password'].value;

    var response = await this.loginService.changePassword(loginCredentials)
    if (!response.result) {
      this.snackbarService.operErrorSnackbar(`Failed to change password. Error: ${response.notes}`);
    }
    this.snackbarService.openSuccessSnackbar('Password changed successfully');
  }

}
