import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { LoginService } from '../../../../Services/login.service';
import { Credentials } from 'src/app/Model/Credentials';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { SecurityQuestion } from 'src/app/Model/SecurityQuestions';

@Component({
    selector: 'app-forgot-password-page',
    templateUrl: './forgot-password-page.component.html',
    styleUrls: ['./forgot-password-page.component.css']
})

export class ForgotPasswordPageComponent implements OnInit {

    public passwordVisible: boolean = false;
    public confirmPasswordVisible: boolean = false;
    public changePasswordVisibilityFlag: boolean = false;
    public securityQuestions: SecurityQuestion[] = [];

    public forgotPasswordForm: FormGroup = new FormGroup({
        response: new FormControl('', Validators.required),
    });

    public changePasswordForm: FormGroup = new FormGroup({
        password: new FormControl('', Validators.required),
        confirmPassword: new FormControl('', [Validators.required, this.notSamePasswordError])
    });

    constructor(
        private loginService: LoginService,
        private snackbarService: SnackBarService
    ) { }

    ngOnInit() {
        this.initQuesitons();
    }

    private async initQuesitons() {
        let secQuestionsResp = await this.loginService.getSecurityQuestions();

        if (!secQuestionsResp.result) {
            this.snackbarService.operErrorSnackbar(`Unable to get security questions. Error: ${secQuestionsResp.notes}`);
        }

        this.securityQuestions = secQuestionsResp.body
            .map(value => ({ value, sort: Math.random() }))
            .sort((a, b) => a.sort - b.sort)
            .map(({ value }) => value);
    }

    public notSamePasswordError(g: FormControl): ValidationErrors {
        let confirmPassword = g.value;
        let password = g.parent?.value?.password;
        g.setErrors({})
        return confirmPassword == password ? {} : { noMatch: true };
    }

    public answer() {
        const answer = this.forgotPasswordForm.controls['response'].value;
        if (answer == this.securityQuestions[0].answer) {
            this.changePasswordVisibilityFlag = true;
        }
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
