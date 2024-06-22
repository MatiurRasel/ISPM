import { NgIf } from '@angular/common';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { AbstractControl, FormsModule, NgForm, ReactiveFormsModule, UntypedFormBuilder, UntypedFormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { Router, RouterLink } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseAlertComponent, FuseAlertType } from '@fuse/components/alert';
import { AuthService } from 'app/core/auth/auth.service';
import { MatRadioModule } from '@angular/material/radio';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { NgxIntlTelInputModule } from 'ngx-intl-tel-input';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

// Uppercase letter validator
function uppercaseValidator(control: AbstractControl): ValidationErrors | null {
    const hasUppercase = /[A-Z]/.test(control.value);
    return hasUppercase ? null : { uppercase: true };
}

// Lowercase letter validator
function lowercaseValidator(control: AbstractControl): ValidationErrors | null {
    const hasLowercase = /[a-z]/.test(control.value);
    return hasLowercase ? null : { lowercase: true };
}

// Symbol validator
function symbolValidator(control: AbstractControl): ValidationErrors | null {
    const hasSymbol = /[!@#$%^&*(),.?":{}|<>]/.test(control.value);
    return hasSymbol ? null : { symbol: true };
}

// Number validator
function numberValidator(control: AbstractControl): ValidationErrors | null {
    const hasNumber = /\d/.test(control.value);
    return hasNumber ? null : { number: true };
}


function minLengthValidator(control: AbstractControl): Promise<ValidationErrors | null> {
    const minLength = 5;
    const value = control.value || '';
    return Promise.resolve(value.length >= minLength ? null : { minlength: { requiredLength: minLength, actualLength: value.length } });
}

function maxLengthValidator(control: AbstractControl): Promise<ValidationErrors | null> {
    const maxLength = 15;
    const value = control.value || '';
    return Promise.resolve(value.length <= maxLength ? null : { maxlength: { requiredLength: maxLength, actualLength: value.length } });
}


@Component({
    selector     : 'auth-sign-up',
    templateUrl  : './sign-up.component.html',
    encapsulation: ViewEncapsulation.None,
    animations   : fuseAnimations,
    standalone   : true,
    imports      : [
        
        RouterLink, NgIf, FuseAlertComponent, 
        FormsModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, 
        MatButtonModule, MatIconModule, MatCheckboxModule, 
        MatProgressSpinnerModule,MatRadioModule, 
        MatDatepickerModule, // Update the import statement here
        //MatDatetimepickerModule
        NgxIntlTelInputModule,
        BsDropdownModule,
        
    ],
})


export class AuthSignUpComponent implements OnInit
{
    

    @ViewChild('signUpNgForm') signUpNgForm: NgForm;

    alert: { type: FuseAlertType; message: string } = {
        type   : 'success',
        message: '',
    };
    signUpForm: UntypedFormGroup;
    showAlert: boolean = false;
    maxDate: Date = new Date();
    /**
     * Constructor
     */
    constructor(
        private _authService: AuthService,
        private _formBuilder: UntypedFormBuilder,
        private _router: Router,
    )
    {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void
    {
       
        // Create the form
        this.signUpForm = this._formBuilder.group({
                gender      : ['M'],
                fullName    : [''],
                userName    : ['', Validators.required],
                //dateOfBirth : ['',Validators.required],
                // city        : [''],
                // country     : [''],
                email       : ['', 
                    [
                        Validators.required, 
                        Validators.email
                    ]
                ],
                password: [
                    '',
                    [
                        Validators.required,
                        Validators.minLength(5),
                        Validators.maxLength(15),
                        uppercaseValidator,
                        lowercaseValidator,
                        symbolValidator,
                        numberValidator,
                    ],
                    minLengthValidator, // Add this line
                    maxLengthValidator  // Add this line
                ],
                confirmPassword: [
                    '',
                    [
                        Validators.required,
                        this.matchValues('password')]],
                        
                //company     : [''],
                dateOfBirth: ['',Validators.required],
                // agreements  : ['', Validators.requiredTrue],
                mobileNumber: [
                    '',
                    [
                        Validators.required, 
                        Validators.pattern('^[1-9][0-9]{9}$'), 
                        //this.firstDigitValidator,
                        //this.noAllZerosValidator
                    ]
                  ],
                },
        );

        this.maxDate.setFullYear(this.maxDate.getFullYear() -18);

        this.signUpForm.controls['password'].valueChanges.subscribe({
            next: () => this.signUpForm.controls['confirmPassword'].updateValueAndValidity()
        });
    }
    
    getEmailErrorMessage(): string {
        const emailControl = this.signUpForm.get('email');

        if (emailControl.hasError('required')) {
            return 'Email address is required';
        }

        if (emailControl.hasError('email')) {
            return 'Please enter a valid email address';
        }
        
    }

    getMobileNumberErrorMessage(): string {
        const mobileNumber = this.signUpForm.get('mobileNumber');

        if (mobileNumber.hasError('required')) {
            return 'Mobile Number is required';
        }
    
        if (mobileNumber.hasError('pattern')) {
            return 'Please enter a valid number';
        }

        // if (mobileNumber.hasError('firstDigitZero')) {
        //     return 'Mobile Number cannot start with 0';
        // }

        // if (mobileNumber.hasError('allZeros')) {
        //     return 'Mobile Number cannot be all zeros';
        // }
    
        return '';
        
    }

    validateNumber(event: KeyboardEvent): void {
        const charCode = event.charCode;
        if (charCode < 48 || charCode > 57) {
          event.preventDefault();
        }
      }
      
    //   noAllZerosValidator(control: AbstractControl): ValidationErrors | null {
    //     const value = control.value;
    //     if (value && /^(0{10})$/.test(value)) {
    //       return { allZeros: true };
    //     }
    //     return null;
    //   }

    // firstDigitValidator(control: AbstractControl): ValidationErrors | null {
    //     const value = control.value;
    //     if (value && value[0] === '0') {
    //       return { firstDigitZero: true };
    //     }
    //     return null;
    //   }
    
    onKeyDown(event: KeyboardEvent): void {
        const inputValue = (event.target as HTMLInputElement).value;
        // Allow numeric characters (0-9) and special keys like Backspace, Delete, Arrow keys
        if (!(
                event.key === 'Backspace' || 
                event.key === 'Delete' || 
                event.key === 'ArrowLeft' || 
                event.key === 'ArrowRight' || 
                event.key === 'ArrowUp' || 
                event.key === 'ArrowDown' || 
                event.key === 'Tab' ||
                (event.key >= '0' && event.key <= '9'))) {
            event.preventDefault();
        }
        // Prevent '0' as the first character
        if (inputValue.length === 0 && event.key === '0') {
            event.preventDefault();
        }
    }
    

    getPasswordErrorMessage(): string {
        const passwordControl = this.signUpForm.get('password');
      
        if (passwordControl.hasError('required')) {
          return 'Password is required';
        }
      
        if (passwordControl.hasError('minlength')) {
          return `Password must be at least ${passwordControl.getError('minlength').requiredLength} characters`;
        }
      
        if (passwordControl.hasError('maxlength')) {
          return `Password must be at most ${passwordControl.getError('maxlength').requiredLength} characters`;
        }
      
        if (passwordControl.hasError('uppercase')) {
          return 'Password must contain at least one uppercase letter';
        }
      
        if (passwordControl.hasError('lowercase')) {
          return 'Password must contain at least one lowercase letter';
        }
      
        if (passwordControl.hasError('symbol')) {
          return 'Password must contain at least one symbol';
        }
      
        if (passwordControl.hasError('number')) {
          return 'Password must contain at least one number';
        }
      
        return '';
    }

    getConfirmPasswordErrorMessage(): string {
        const confirmPasswordControl = this.signUpForm.get('confirmPassword');
    
        if (confirmPasswordControl.hasError('required')) {
            return 'Confirm Password is required';
        }
    
        if (confirmPasswordControl.hasError('notMatching')) {
            return 'Passwords do not match';
        }
    
        // You can add more conditions based on your specific requirements for Confirm Password
    
        return '';
    }
    
    matchValues(matchTo: string):ValidatorFn{
        return(control:AbstractControl) => {
          return control.value === control.parent?.get(matchTo)?.value
          ? null : {notMatching: true}
        }
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Sign up
     */
    signUp(): void
    {
        debugger
        // Mark all form controls as touched to trigger error messages
        this.signUpForm.markAllAsTouched();
        // Do nothing if the form is invalid
        if ( this.signUpForm.invalid )
        {
            return;
        }

        const dob = this.getDateOnly(this.signUpForm.controls['dateOfBirth'].value);//yyyy-mm-dd


        // Disable the form
        this.signUpForm.disable();

        // Hide the alert
        this.showAlert = false;
        this.signUpForm.controls['dateOfBirth'].setValue(dob);
        //const values = {...this.signUpForm.value,dateOfBirth: dob};
        // Sign up
        //this._authService.register(values).subscribe();
        debugger
        this._authService.signUp(this.signUpForm.value)
        .subscribe(
          (response) => {
            console.log('Sign Up Response:', response);
            // Navigate to the confirmation required page
            this._router.navigateByUrl('/signed-in-redirect');
          },
        (error) => {
            // Error scenario
            console.error('Error:', error);

            if (error.error && error.error.errors) {
                // Handle specific field errors from ModelState
                Object.keys(error.error.errors).forEach(key => {
                  const formControl = this.signUpForm.get(key);
                  if (formControl) {
                    formControl.setErrors({
                      serverError: error.error.errors[key]
                    });
                  }
                });

                 // Display the first field-specific error message
                 const firstErrorKey = Object.keys(error.error.errors)[0];
                 const firstErrorMessage = error.error.errors[firstErrorKey];
                 
                 this.alert = {
                     type: 'error',
                     message: firstErrorMessage
                 };
              } else {
                // Handle generic error messages
                this.alert = {
                  type: 'error',
                  message: 'Something went wrong, please try again.'
                };
              }
              
              this.showAlert = true;

            // Re-enable the form
            this.signUpForm.enable();

            // Reset the form with default values
            // this.signUpNgForm.resetForm({
            //     gender: 'M',
            //     fullName: '',
            //     userName: '',
            //     email: '',
            //     password: '',
            //     confirmPassword: '',
            //     dateOfBirth: '',
            //     mobileNumber: '',
            // });

            // Set the alert
            // this.alert = {
            //     type: 'error',
            //     message: 'Something went wrong, please try again.',
            // };

            // Show the alert
            //this.showAlert = true;
        }
    );

    }

    private getDateOnly(dob: string | undefined) {
        if(!dob) return;
        let theDob = new Date(dob);
        return new Date(theDob.setMinutes(
          theDob.getMinutes()-theDob.getTimezoneOffset()
          )).toISOString().slice(0,10);
    
      }
}
