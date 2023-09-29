import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Credentials } from 'src/app/Model/Credentials';
import { User } from 'src/app/Model/User';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { LoginService } from 'src/app/Services/login.service';

@Component({
  selector: 'app-user-information',
  templateUrl: './user-information.component.html',
  styleUrls: ['./user-information.component.css']
})
export class UserInformationComponent implements OnInit {

  @Output() onConfirm: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();


  public form: FormGroup = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
    name: new FormControl(''),
    surname: new FormControl(''),
    email: new FormControl('', [Validators.required, Validators.email]),
    confirmPassword: new FormControl('', [Validators.required, this.notSamePasswordError])
  });

  constructor(
    private loginService: LoginService,
    private snackbarService: SnackBarService
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
    if (!this.form.valid) {
      this.snackbarService.operErrorSnackbar('Fill the form correctly.');
      return;
    }

    let credentials: Credentials = new Credentials(this.form?.controls['username'].value, this.form?.controls['password'].value);
    let user: User = new User();

    user.name = this.form?.controls['name'].value;
    user.surname = this.form?.controls['surname'].value;
    user.email = this.form?.controls['email'].value;
    user.credentials = credentials;

    this.loginService.user = user;
    this.onConfirm.emit();
  }

}
