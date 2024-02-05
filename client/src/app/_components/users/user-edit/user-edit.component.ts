import { Component, HostListener, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent {

  @ViewChild('editForm') editForm: NgForm |undefined;
  @HostListener('window:beforeunload',['$event']) unloadNotification($event:any) {
    if(this.editForm?.dirty) {
      $event.returnValue = true;
    }
  }
}
