import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { RegistrationCredentials } from 'src/app/Model/RegistraitonCredentials';
import { LoginService } from 'src/app/Pages/login/Services/login.service';

@Component({
  selector: 'app-user-information',
  templateUrl: './user-information.component.html',
  styleUrls: ['./user-information.component.css']
})
export class UserInformationComponent implements OnInit {

  @Output() onConfirm: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();


  public form: FormGroup = new FormGroup({
    username: new FormControl('', Validators.required),
    name: new FormControl(''),
    surname: new FormControl(''),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required),
    confirmPassword: new FormControl('', [Validators.required, this.notSamePasswordError])
  });

  constructor(
    private loginService: LoginService
  ) { }

  ngOnInit() {
  }
  public passwordVisible: boolean = false;
  public confirmPasswordVisible: boolean = false;

  public notSamePasswordError(g: FormControl): ValidationErrors {
    let confirmPassword = g.value;
    let password = g.parent?.value?.password;
    g.setErrors({})
    return confirmPassword == password ? {} : {noMatch: true};
  }

  public register() {
    let registrationCredentials: RegistrationCredentials = new RegistrationCredentials();
    registrationCredentials.username = this.form?.controls['username'].value;
    registrationCredentials.name = this.form?.controls['name'].value;
    registrationCredentials.surname = this.form?.controls['surname'].value;
    registrationCredentials.email = this.form?.controls['email'].value;
    registrationCredentials.password = this.form?.controls['password'].value;
    this.loginService.registrationCredentials = registrationCredentials;
    this.onConfirm.emit();
  }

}
