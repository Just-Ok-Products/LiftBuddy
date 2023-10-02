import { Component, Input, OnInit, forwardRef } from '@angular/core';
import { FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { LoadingVisualizationService } from 'src/app/Services/loading-visualization.service';

@Component({
  selector: 'app-setting-row',
  templateUrl: './setting-row.component.html',
  styleUrls: ['./setting-row.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SettingRowComponent),
      multi: true,
    }
  ]
})
export class SettingRowComponent implements OnInit {

  @Input() label: string = '';
  @Input() doesLoading: boolean = false;
  @Input() formControl: FormControl = new FormControl(null);
  @Input() default: any = '';
  @Input() options: {value: string | boolean, label: string}[] = [];

  constructor(
    private loadingVisualizationService: LoadingVisualizationService
  ) { }

  ngOnInit() {
    this.formControl.setValue(this.default);
  }

  public updateForm(value: any) {
    this.loadingVisualizationService.setIsLoading(true && this.doesLoading);
    this.formControl.setValue(value);

    // TODO: reload page with new language

    this.loadingVisualizationService.setIsLoading(false);
  }

  // Don't know what this does but it's mandatory for the provider

  writeValue(value: string) {
    this.default = value ? value : '';
  }

  registerOnChange() {}

  registerOnTouched() {}

}
