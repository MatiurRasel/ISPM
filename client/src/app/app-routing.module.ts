import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './_components/home/home.component';
import { LandingMainComponent } from './landing/landing-main/landing-main.component';

const routes: Routes = [
  //{path:'',component:HomeComponent},
  {path:'',component:LandingMainComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
