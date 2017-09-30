import { NgModule } from '@angular/core';
import { Routes,RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login.component';
export const routes: Routes = [
   //localhost:xxxx/login
  { path: '', component: LoginComponent }
];
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  declarations: [LoginComponent]
})
export class LoginModule { }