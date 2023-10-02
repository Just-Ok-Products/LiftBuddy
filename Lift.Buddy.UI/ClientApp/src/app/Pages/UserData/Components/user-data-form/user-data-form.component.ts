import { Component, Input, OnInit, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { User } from 'src/app/Model/User';

@Component({
    selector: 'app-user-data-form',
    templateUrl: './user-data-form.component.html',
    styleUrls: ['./user-data-form.component.css']
})

export class UserDataFormComponent implements OnInit {

    @Input() userData: User | undefined;

    public userDataForm: FormGroup = new FormGroup({
        username: new FormControl(''),
        name: new FormControl(''),
        surname: new FormControl(''),
        email: new FormControl(''),
    });

    constructor() { }

    ngOnInit() {
        this.initFormData();
        this.initFormChangeSubs();
    }

    private initFormData() {
        if (this.userData == null) {
            return;
        }

        this.userDataForm.controls['username'].setValue(this.userData?.credentials.username ?? '');
        this.userDataForm.controls['name'].setValue(this.userData?.name ?? '');
        this.userDataForm.controls['surname'].setValue(this.userData?.surname ?? '');
        this.userDataForm.controls['email'].setValue(this.userData?.email ?? '');
    }

    private initFormChangeSubs() {
        this.userDataForm.valueChanges.subscribe(value => {
            if (this.userData == undefined) {
                this.userData = new User();
            }

            this.userData.username = value.username;
            this.userData.name = value.name;
            this.userData.surname = value.surname;
            this.userData.email = value.email;
        })
    }

}
