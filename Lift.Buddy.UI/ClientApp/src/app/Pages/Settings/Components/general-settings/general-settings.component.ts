import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { DialogService } from 'src/app/Services/Utils/dialog.service';
import { LoginService } from 'src/app/Services/login.service';
import { SwapRoleConfirmationDialogComponent } from './Components/swap-role-confirmation-dialog/swap-role-confirmation-dialog.component';
import { LoadingVisualizationService } from 'src/app/Services/loading-visualization.service';

@Component({
  selector: 'app-general-settings',
  templateUrl: './general-settings.component.html',
  styleUrls: ['./general-settings.component.css']
})
export class GeneralSettingsComponent implements OnInit {

  constructor(
    private loginService: LoginService,
    private dialogService: DialogService,
    private loadingVisService: LoadingVisualizationService
  ) { }

  ngOnInit() {
    this.loadingVisService.$isLoading.subscribe(loading => {
      this.isLoading = loading;
    })
  }

  public isTrainer = this.loginService.isTrainer;
  public isLoading: boolean = false;

  public language = 'en';
  public languages = [
    {value: 'en', label: 'English'},
    {value: 'it', label: 'Italiano'},
  ];

  public UOM = true;
  public UOMs = [
    {value: 'kg', label: 'KG'},
    {value: 'lb', label: 'LB'},
  ];

  public generalSettingsForm: FormGroup = new FormGroup({
    UOM: new FormControl('kg'),
    language: new FormControl('en')
  });

  public swapRole() {
    const dialogRef = this.dialogService.openCenterDialog(SwapRoleConfirmationDialogComponent, {});

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.isTrainer = !this.isTrainer;
      }
    })
  }

  public getFormControl(controlName: string) {
    return this.generalSettingsForm.get(controlName) as FormControl;
  }

  public reset() {

  }

  public save() {

  }
}
