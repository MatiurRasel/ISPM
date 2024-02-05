import { Component, Input, Self } from "@angular/core";
import { ControlValueAccessor, NgControl } from "@angular/forms";


@Component({
    selector: 'DS2',
    template: `
       <ng-select
          [items]="options"
          [bindLabel]="labelField"
          [bindValue]="valueField"
          [(ngModel)]="selectedOption"
          (change)="onChange()"
          class="w-full md:w-64 px-4 py-2 leading-tight focus:outline-none focus:border-blue-500 border rounded-md"
        >
          <ng-option [value]="null">Select {{ labelField }}</ng-option>
        </ng-select>

        <div class="invalid-feedback mt-2 md:ml-4" *ngIf="ngControl.touched && (ngControl.hasError('required') || selectedOption === null)">
          Please select a {{ labelField }}
        </div>

        
    `,
})

export class Select2Component implements ControlValueAccessor{
    
    @Input() options: any[] = [];
    @Input() labelField: string = 'label';
    @Input() valueField: string = 'value';

    selectedOption: any;

    constructor(@Self() public ngControl: NgControl) {
        this.ngControl.valueAccessor = this;
    }

    onChange() {
        this.propagateChange(this.selectedOption);
        console.log('onChange called. selectedOption:', this.selectedOption);
    }

    writeValue(obj: any): void {
        this.selectedOption = obj;
        console.log('writeValue called. selectedOption:', this.selectedOption);
    }
    registerOnChange(fn: any): void {
        //this.propagateChange = fn;
    }
    registerOnTouched(fn: any): void {
        // Not needed for ng-select
    }
    setDisabledState?(isDisabled: boolean): void {
        // Not needed for ng-select
    }

    private propagateChange = (_: any) => {};
}
   