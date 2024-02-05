import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'DDP-BS',
  template: `
  <div class="mb-4 md:flex md:items-center">
    <input
      type="text"
      [class.is-invalid]="control.touched && control.invalid"
      class="form-input w-full md:w-64 px-4 py-2 leading-tight focus:outline-none focus:border-blue-500 border rounded-md"
      [formControl]="control"
      placeholder="{{ label }}"
      bsDatepicker
      [bsConfig]="bsConfig"
      [maxDate]="maxDate"
    >

    <div class="invalid-feedback mt-2 md:ml-4" *ngIf="control.errors?.['required']">
        {{label}} is required
    </div>
    <!-- Add more validation messages as needed -->

  </div>
  `
})
export class DatePickerComponent implements ControlValueAccessor{
  @Input() label = '';
  @Input() maxDate: Date | undefined;
  bsConfig: Partial<BsDatepickerConfig> | undefined;

  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
    this.bsConfig = {
      containerClass:'theme-red',
      dateInputFormat: 'DD-MM-YYYY'
    }
  }
  writeValue(obj: any): void {
    //throw new Error('Method not implemented.');
  }
  registerOnChange(fn: any): void {
    //throw new Error('Method not implemented.');
  }
  registerOnTouched(fn: any): void {
    //throw new Error('Method not implemented.');
  }
  setDisabledState?(isDisabled: boolean): void {
    //throw new Error('Method not implemented.');
  }
  
  get control(): FormControl {
    return this.ngControl.control as FormControl;
  }
}
