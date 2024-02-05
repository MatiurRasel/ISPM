import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  model:any = {};

  constructor(public accountService: AccountService,
    private router:Router,
    private toastr: ToastrService){}

  
  login(){
    this.accountService.login(this.model).subscribe({
      next:() => {
        this.router.navigateByUrl('/dashboard');
        this.model = {};
      }
    })
  }

  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
    
}
