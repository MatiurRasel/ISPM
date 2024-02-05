import { Component, Input } from "@angular/core";
import { FormControl } from "@angular/forms";



@Component({
    selector:'dynamic-input',
    template: `
        <div class="mb-4">
          <label 
          [class]="labelClass ? labelClass : defaultLabelClass">{{ label }}</label>
          <input
            [type]="inputType"
            [class]="inputClass ? inputClass : defaultInputClass"
            [formControl]="control"
            [maxLength]="maxLength"
          />
          <div *ngIf="!isValid" class="text-red-400 text-sm mt-1">
            {{ getValidationMessage() }}
          </div>
        </div>
    `
})

export class DynamicInputComponent {

    @Input() label: string = '';
    @Input() labelClass: string | null = null;
    @Input() inputClass: string | null = null;
    @Input() control: FormControl = new FormControl();
    @Input() validationMessages: { [key: string]: string } = {};
    @Input() maxLength: number = 255;
    @Input() inputType: string = 'text'; // Default to text input type
    
    defaultLabelClass: string = 'block text-gray-700 text-sm font-bold mb-2';
    defaultInputClass: string = 'shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline';

    get isValid(): boolean {
      return this.control.valid || !this.control.touched;
    }

    getValidationMessage(): string {
      for (const errorKey in this.control.errors) {
        if (this.control.errors.hasOwnProperty(errorKey) && !this.isValid) {
          return this.validationMessages[errorKey];
        }
      }
      return '';
    }
}