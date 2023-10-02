import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Credentials } from 'src/app/Model/Credentials';
import { SecurityQuestion } from 'src/app/Model/SecurityQuestions';
import { User } from 'src/app/Model/User';
import { LoginService } from 'src/app/Services/login.service';

@Component({
    selector: 'app-security-questions',
    templateUrl: './security-questions.component.html',
    styleUrls: ['./security-questions.component.css']
})
export class SecurityQuestionsComponent implements OnInit {

    constructor(
        private loginService: LoginService
    ) { }

    ngOnInit() {
    }

    @Input() registrationStep: number | undefined;
    @Output() onRegister: EventEmitter<Credentials> = new EventEmitter<Credentials>();
    @Output() onBack: EventEmitter<boolean> = new EventEmitter();

    public questions: string[] = ["test", "t", "e", "s", "t"]
    public items: number[] = [1, 2, 3]


    public form: FormGroup = new FormGroup({
        question1: new FormControl('', Validators.required),
        question2: new FormControl('', Validators.required),
        question3: new FormControl('', Validators.required),
        response1: new FormControl('', [Validators.required]),
        response2: new FormControl('', [Validators.required]),
        response3: new FormControl('', [Validators.required]),
    });

    public register() {
        let userWithoutSecQuestions = this.loginService.user!;
        let securityQuestions: SecurityQuestion[] = [];

        console.log('question' + 1)
        if (this.form.valid) {
            securityQuestions = this.items
                .map(n => new SecurityQuestion(this.form.controls['question' + n].value, this.form.controls['response' + n].value));
        }

        let userToRegister: User = {
            id: "",
            username: userWithoutSecQuestions.username,
            name: userWithoutSecQuestions.name,
            surname: userWithoutSecQuestions.surname,
            email: userWithoutSecQuestions.email,
            credentials: userWithoutSecQuestions.credentials,
            securityQuestions: securityQuestions
        }

        this.loginService.user = userToRegister;
        this.onRegister.emit();
    }


    public back() {
        this.onBack.emit();
    }

}
