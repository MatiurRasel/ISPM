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
    imports      : [RouterLink, NgIf, FuseAlertComponent, FormsModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatIconModule, MatCheckboxModule, MatProgressSpinnerModule,MatRadioModule],
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
                gender      : ['male'],
                fullName    : ['', Validators.required],
                userName    : ['', Validators.required],
                dateOfBirth : ['',Validators.required],
                city        : [''],
                country     : [''],
                email       : ['', [Validators.required, Validators.email]],
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
                confirmPassword: ['',[Validators.required ,this.matchValues('password')]],
                company     : [''],
                agreements  : ['', Validators.requiredTrue],
            },
        );

        this.signUpForm.controls['password'].valueChanges.subscribe({
            next: () => this.signUpForm.controls['confirmPassword'].updateValueAndValidity()
        })
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
        // Do nothing if the form is invalid
        if ( this.signUpForm.invalid )
        {
            return;
        }

        // Disable the form
        this.signUpForm.disable();

        // Hide the alert
        this.showAlert = false;

        // Sign up
        this._authService.signUp(this.signUpForm.value)
            .subscribe(
                (response) =>
                {
                    // Navigate to the confirmation required page
                    this._router.navigateByUrl('/confirmation-required');
                },
                (response) =>
                {
                    // Re-enable the form
                    this.signUpForm.enable();

                    // Reset the form
                    this.signUpNgForm.resetForm();

                    // Set the alert
                    this.alert = {
                        type   : 'error',
                        message: 'Something went wrong, please try again.',
                    };

                    // Show the alert
                    this.showAlert = true;
                },
            );
    }
}
