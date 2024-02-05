import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import flatpickr from 'flatpickr';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {
  registerForm: FormGroup = new FormGroup({});
  customForm: FormGroup = new FormGroup({});
  maxDate: Date = new Date();

  //myForm!: FormGroup;

  dropdownOptions: any[] = [
    { label: '--', value: '' },
    { label: 'Option 1', value: 'option1' },
    { label: 'Option 2', value: 'option2' },
    // Add more options as needed
  ];

  optionsArray: any[] = [
    { label: 'Option 1', value: 'option1' },
    { label: 'Option 2', value: 'option2' },
    // Add more options as needed
  ];

  selectedOptions: any[] = []; // Initialize as an empty array

  @ViewChild('datetimePickerInput') datetimePickerInput!: ElementRef;

  constructor(private fb: FormBuilder) {
    
  }
  ngOnInit(): void {
    this.initializeForm();
    this.maxDate.setFullYear(this.maxDate.getFullYear() -18);
    this.initializeCustomForm();
    
  }
  initializeCustomForm() {
    this.customForm = this.fb.group({
      firstName: this.fb.control('', [Validators.required]),
      lastName: this.fb.control('', [Validators.required, Validators.pattern(/^\d+$/)]),
    })
  }

  ngOnDestroy() {
    // Destroy the Flatpickr instance
    const flatpickrInstance = flatpickr(this.datetimePickerInput.nativeElement);
    flatpickrInstance.destroy();
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      firstName: [''],
      lastName: [''],
      userName: ['',Validators.required],
      email:['',Validators.required],      
      dateOfBirth: ['',Validators.required],
      dateOfBirth2: ['',Validators.required],
      userRole: ['',Validators.required],
      userRole2: [null,Validators.required],
    })

  }


}

