import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Credentials } from 'src/app/Model/Credentials';
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

  public form: FormGroup = new FormGroup({
    question1: new FormControl('', Validators.required),
    response1: new FormControl('', [Validators.required]),
    question2: new FormControl('', Validators.required),
    response2: new FormControl('', [Validators.required]),
    question3: new FormControl('', Validators.required),
    response3: new FormControl('', [Validators.required]),
  });

//   public register() {
//     if (this.form.valid) {
//       let credentials = this.loginService.credentials;
//       let questions = [], answers = [];
//       questions.push(this.form.controls['question1'].value)
//       questions.push(this.form.controls['question2'].value)
//       questions.push(this.form.controls['question3'].value)
//       answers.push(this.form.controls['response1'].value)
//       answers.push(this.form.controls['response2'].value)
//       answers.push(this.form.controls['response3'].value)
        
//       let user : User = {username = credentials.username, credentials.password, }
//       credentials!.answers = answers;
//       credentials!.questions = questions;

//       this.loginService.credentials = credentials;
//       this.onRegister.emit();
//     }
//   }

  public back() {
    this.onBack.emit();
  }

}
