import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
  selector: 'DTI-Default',
  template: ` 
  <div class="mb-4 md:flex md:items-center">
    <input
        type="{{ type }}"
        [class.is-invalid]="control.touched && control.invalid"
        class="form-input w-full md:w-64 px-4 py-2 leading-tight focus:outline-none focus:border-blue-500 border rounded-md"
        [formControl]="control"
        placeholder="{{ label }}"
    >

    <div class="invalid-feedback mt-2 md:ml-4" *ngIf="control.errors?.['required']">
        Please enter a {{ label }}
    </div>
    <div class="invalid-feedback mt-2 md:ml-4" *ngIf="control.errors?.['minlength']">
        {{ label }} must be at least {{ control.errors?.['minlength'].requiredLength }} characters
    </div>
    <div class="invalid-feedback mt-2 md:ml-4" *ngIf="control.errors?.['maxlength']">
      {{ label }} must be at most {{ control.errors?.['maxlength'].requiredLength }} characters
    </div>
    <div class="invalid-feedback mt-2 md:ml-4" *ngIf="control.errors?.['notMatching']">
      Passwords do not match
    </div>
  </div>
  `,
})
export class TextInputComponentDefault implements ControlValueAccessor {
  @Input() label = '';
  @Input() type = 'text';

  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
  }
  
  writeValue(obj: any): void {
    //throw new Error('Method not implemented.');
    //this.control.setValue(obj); // Set the value of the underlying form control
  }
  registerOnChange(fn: any): void {
    //throw new Error('Method not implemented.');
    //this.control.valueChanges.subscribe(fn); // Notify the parent form about changes
  }
  registerOnTouched(fn: any): void {
    //throw new Error('Method not implemented.');
    //this.control.markAsTouched(); // Mark the control as touched
    //fn(); // Notify the parent form about being touched
  }
  setDisabledState?(isDisabled: boolean): void {
    //throw new Error('Method not implemented.');
    // if (isdisabled) {
    //  this.control.disable(); // disable the form control
    // } else {
    //  this.control.enable(); // enable the form control
    // }
  }

  get control(): FormControl {
    return this.ngControl.control as FormControl
  }

}

