import { Component, Input, Self } from "@angular/core";
import { ControlValueAccessor, FormControl, NgControl, Validators } from "@angular/forms";


@Component({
    selector:'DDD',
    template: `
    <div class="mb-4 md:flex md:items-center">
      <select
        [class.is-invalid]="control.touched && control.invalid"
        class="form-select w-full md:w-64 px-4 py-2 leading-tight focus:outline-none focus:border-blue-500 border rounded-md"
        [formControl]="control"
      >
        <option [ngValue]="null" disabled>Select {{ label }}</option>
        <option *ngFor="let option of options" [ngValue]="option.value">{{ option.label }}</option>
      </select>

      <div class="invalid-feedback mt-2 md:ml-4" *ngIf="control.errors?.['required']">
        Please select a {{ label }}
      </div>
    </div>
  `,
})

export class DynamicDropDownComponent implements ControlValueAccessor {

    @Input() label = '';
    @Input() options: { label: string; value: any }[] = [];

    // Declare the form control property with the correct type
    //@Input() formControl!: FormControl; // Ensure this is FormControl type

    constructor(@Self() public ngControl: NgControl) {
        this.ngControl.valueAccessor = this;
    }

    writeValue(obj: any): void {
        // Set the value of the underlying form control
    //this.control.setValue(obj);
    }
    registerOnChange(fn: any): void {
       // Notify the parent form about changes
    //this.control.valueChanges.subscribe(fn);
    }
    registerOnTouched(fn: any): void {
       // Mark the control as touched
    //this.control.markAsTouched();
    // Notify the parent form about being touched
    //fn();
    }
    setDisabledState?(isDisabled: boolean): void {
       // Enable/disable the form control based on the provided value
    //isDisabled ? this.control.disable() : this.control.enable();
  
    }

    get control(): FormControl {
        return this.ngControl.control as FormControl;
    }
}