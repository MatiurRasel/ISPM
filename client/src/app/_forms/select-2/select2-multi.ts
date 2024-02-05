import { Component, Input, Self } from "@angular/core";
import { ControlValueAccessor, NgControl } from "@angular/forms";


@Component({
    selector:'DS2M',
    template: `
         <ng-multiselect-dropdown
              [placeholder]="placeholder"
              [data]="options"
              [(ngModel)]="selectedItems"
              (onSelect)="onItemSelect($event)"
              (onDeSelect)="onItemDeSelect($event)"
              (onSelectAll)="onSelectAll($event)"
              (onDeSelectAll)="onDeSelectAll($event)"
            ></ng-multiselect-dropdown>
    `,
})

export class Select2MultiSelectComponent implements ControlValueAccessor {
    
    @Input() placeholder: string = '';
    @Input() options: any[] = [];

    selectedItems: any[] = [];

    constructor(@Self() public ngControl: NgControl) {
        this.ngControl.valueAccessor = this;
    }

    onItemSelect(item: any): void {
        this.propagateChange(this.selectedItems);
    }
    
    onItemDeSelect(item: any): void {
      this.propagateChange(this.selectedItems);
    }

    onSelectAll(items: any): void {
      this.propagateChange(this.selectedItems);
    }

    onDeSelectAll(items: any): void {
      this.propagateChange(this.selectedItems);
    }

    writeValue(obj: any): void {
      if (obj) {
        this.selectedItems = obj;
      }
    }

    registerOnChange(fn: any): void {
      this.propagateChange = fn;
    }

    registerOnTouched(fn: any): void {
      // Not needed for ng-multiselect-dropdown
    }

    setDisabledState?(isDisabled: boolean): void {
      // Not needed for ng-multiselect-dropdown
    }

    private propagateChange = (_: any) => {};
}