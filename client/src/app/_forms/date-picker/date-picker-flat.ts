import { AfterViewInit, Component, ElementRef, Input, OnInit, Self, ViewChild } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';
import flatpickr from 'flatpickr';

@Component({
  selector: 'DDP-Flat',
  template : `
   <div class="mb-4 md:flex md:items-center">
    <input
    #datetimePickerInput
      type="text"
      [class.is-invalid]="control.touched && control.invalid"
      class="form-input w-full md:w-64 px-4 py-2 leading-tight focus:outline-none focus:border-blue-500 border rounded-md"
      [formControl]="control"
      placeholder="{{ label }}"
      
    >

    <div class="invalid-feedback mt-2 md:ml-4" *ngIf="control.errors?.['required']">
        {{label}} is required
    </div>
    <!-- Add more validation messages as needed -->

</div> 
  `
})
export class DatePickerAltComponent implements ControlValueAccessor,AfterViewInit  {
  @ViewChild('datetimePickerInput') datetimePickerInput!: ElementRef;
  
  @Input() label = '';
  @Input() maxDate: Date | undefined;

  //constructor(@Self() public ngControl: NgControl) {
  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
    
  }
  ngAfterViewInit() {
    if (this.datetimePickerInput && this.datetimePickerInput.nativeElement) {
      this.initializeDateTimePicker();
    }
  }

  // ngOnInit() {
  //   this.initializeDateTimePicker();
  // }

  initializeDateTimePicker() {
    flatpickr(this.datetimePickerInput.nativeElement, {
      //enableTime: true,
      dateFormat: 'd-m-Y',
      maxDate: this.maxDate, // Maximum date allowed
      // Add more options as needed
    });
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
