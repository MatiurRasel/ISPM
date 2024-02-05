import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';
import { fadeInOut } from 'src/app/animation/animations';

@Component({
  selector: 'DTI',
  template: ` 
     <div class="mb-4 pl-2">
      <label class="block text-sm font-medium text-black-600 mb-1 relative">
        {{ label }}
        <span *ngIf="shouldShowError()" [@fadeInOut] class="ml-2 inline-block text-red-500 opacity-0 group-hover:opacity-100 transition-opacity text-sm">
          ({{ control.errors | json }})
        </span>
      </label>
      <div class="relative">
        <input
          type="{{ type }}"
          [class.is-invalid]="control.touched && control.invalid && !control.dirty "
          class="form-input w-full px-4 py-2 pl-2 leading-tight focus:outline-none border-b border-gray-500 border-solid border-t-0 border-r-0 border-l-0 border-0"
          [formControl]="control"
          placeholder="{{ label }}"
        >
      </div>
    </div>

  `,
  animations: [fadeInOut],
  
  styles: `
  .animated-message {
      opacity: 1;
    }
  `,
  
})
export class TextInputComponent implements ControlValueAccessor {
  @Input() label = '';
  @Input() type = 'text';
  hasAnimationRun: boolean = false; // Add this line

  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
  }

  shouldShowError(): boolean {
    const isTouchedAndDirty = this.control.touched && this.control.dirty;
    const isEmptyValue = this.control.value === '' || this.control.value === null;
  
    // Show error if touched, dirty, and has an empty value
    return isTouchedAndDirty && isEmptyValue;
  }

  writeValue(obj: any): void {
    //throw new Error('Method not implemented.');
    this.control.setValue(obj); // Set the value of the underlying form control
  }
  registerOnChange(fn: any): void {
    //throw new Error('Method not implemented.');
    this.control.valueChanges.subscribe(fn); // Notify the parent form about changes
  }
  registerOnTouched(fn: any): void {
    //throw new Error('Method not implemented.');
    this.control.markAsTouched(); // Mark the control as touched
    fn(); // Notify the parent form about being touched
  }
  setDisabledState?(isDisabled: boolean): void {
    //throw new Error('Method not implemented.');
    if (isDisabled) {
     this.control.disable(); // disable the form control
    } else {
     this.control.enable(); // enable the form control
    }
  }

  get control(): FormControl {
    return this.ngControl.control as FormControl
  }

}
