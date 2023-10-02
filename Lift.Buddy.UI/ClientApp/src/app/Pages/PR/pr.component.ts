import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { PersonalRecord } from 'src/app/Model/PersonalRecord';
import { UserPersonalRecord } from 'src/app/Model/UserPersonalRecord';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { PrService } from 'src/app/Services/pr.service';

@Component({
  selector: 'app-pr',
  templateUrl: './pr.component.html',
  styleUrls: ['./pr.component.css']
})
export class PrComponent implements OnInit {

  constructor(
    private prService: PrService,
    private snackbarService: SnackBarService
  ) { }

  public prForm: FormGroup = new FormGroup({
    exercizes: new FormControl<PersonalRecord[]>([])
  });

  async ngOnInit() {
    await this.initUserPR();
  }

  private isUpdate: boolean = false;
  private async initUserPR() {
    const prResp = await this.prService.get();

    if (!prResp.result) {
      this.snackbarService.operErrorSnackbar(`Failed to load PR. Error ${prResp.notes}`)
      return;
    }

    if (prResp.body.length == 0) {
      return;
    }

    this.isUpdate = true;
    this.prForm.controls['exercizes'].setValue(prResp.body[0].personalRecords);
  }

  public addExercize() {
    this.prForm.controls['exercizes'].value.push(new PersonalRecord());
  }

  public async save() {
    let userPR = new UserPersonalRecord();

    userPR.personalRecords = this.prForm.controls['exercizes'].value;

    const saveResp = await this.prService.savePR(userPR, this.isUpdate);

    if (!saveResp.result) {
      this.snackbarService.operErrorSnackbar(`An exception occurred during save. Ex ${saveResp.notes}`);
      return;
    }

    this.snackbarService.openSuccessSnackbar('Personal results succesfully saved.')
  }

}
