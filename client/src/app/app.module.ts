import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardComponent } from './_components/dashboard/dashboard.component';
import { NavComponent } from './_components/nav/nav.component';
import { HasRoleDirective } from './_directives/has-role.directive';
import { NotFoundComponent } from './_errors/not-found/not-found.component';
import { ServerErrorComponent } from './_errors/server-error/server-error.component';
import { TestErrorComponent } from './_errors/test-error/test-error.component';
import { HomeComponent } from './_components/home/home.component';
import { UserEditComponent } from './_components/users/user-edit/user-edit.component';
import { UserDetailComponent } from './_components/users/user-detail/user-detail.component';
import { PhotoEditorComponent } from './_components/users/photo-editor/photo-editor.component';
import { SharedModule } from './_modules/shared.module';
import { HttpClientModule } from '@angular/common/http';
import { TextInputComponent } from './_forms/text-input/text-input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DatePickerComponent } from './_forms/date-picker/date-picker-bs';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DatePickerAltComponent } from './_forms/date-picker/date-picker-flat';
import { DynamicDropDownComponent } from './_forms/drop-down/dropdown';
import { NgSelectModule } from '@ng-select/ng-select';
import { Select2Component } from './_forms/select-2/select2';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { Select2MultiSelectComponent } from './_forms/select-2/select2-multi';
import { TextInputComponentDefault } from './_forms/text-input/text-input-default';
import { DynamicInputComponent } from './_forms/text-input/dynamic-input';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    NavComponent,
    HasRoleDirective,
    NotFoundComponent,
    ServerErrorComponent,
    TestErrorComponent,
    HomeComponent,
    UserEditComponent,
    UserDetailComponent,
    PhotoEditorComponent,
    TextInputComponent,
    DatePickerComponent,
    DatePickerAltComponent,
    DynamicDropDownComponent,
    Select2Component,
    Select2MultiSelectComponent,
    TextInputComponentDefault,
    DynamicInputComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    BrowserAnimationsModule,
    NgSelectModule,
    NgMultiSelectDropDownModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
