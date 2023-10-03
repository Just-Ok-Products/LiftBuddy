import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { PersonalRecord } from 'src/app/Model/PersonalRecord';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { LoginService } from 'src/app/Services/login.service';
import { PersonalRecordService } from 'src/app/Services/pr.service';

@Component({
    selector: 'app-pr',
    templateUrl: './pr.component.html',
    styleUrls: ['./pr.component.css']
})

export class PersonalRecordComponent implements OnInit {

    constructor(
        private personalRecordService: PersonalRecordService,
        private snackbarService: SnackBarService
    ) { }

    async ngOnInit() {
        await this.initUserPersonalRecord();
    }

    public prForm: FormGroup = new FormGroup({
        exercises: new FormControl<PersonalRecord[]>([])
    });

    private async initUserPersonalRecord() {
        const response = await this.personalRecordService.get();

        if (!response.result) {
            this.snackbarService.operErrorSnackbar(`Failed to load PR. Error ${response.notes}`)
            return;
        }

        if (response.body.length == 0) {
            return;
        }

        this.prForm.controls['exercises'].setValue(response.body);
    }

    public addExercise() {
        this.prForm.controls['exercises'].value.push(new PersonalRecord());
    }

    public async add() {
        const records = this.prForm.controls['exercises'].value;
        const saveResp = await this.personalRecordService.addPersonalRecord(records);

        if (!saveResp.result) {
            this.snackbarService.operErrorSnackbar(`An exception occurred during save. Ex ${saveResp.notes}`);
            return;
        }

        this.snackbarService.openSuccessSnackbar('Personal results succesfully saved.')
    }

    public async update() {
        const records = this.prForm.controls['exercises'].value;
        const saveResp = await this.personalRecordService.updatePersonalRecord(records);

        if (!saveResp.result) {
            this.snackbarService.operErrorSnackbar(`An exception occurred during save. Ex ${saveResp.notes}`);
            return;
        }

        this.snackbarService.openSuccessSnackbar('Personal results succesfully saved.')
    }
}
