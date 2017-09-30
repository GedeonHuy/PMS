import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserComponent } from './user.component';
import { Routes, RouterModule } from '@angular/router';

const UserRoutes: Routes = [
   //localhost:xxxx/main/user
  { path: '', redirectTo: 'index', pathMatch: 'full' },
   //localhost:xxxx/main/user/index
  { path: 'index', component: UserComponent }
]
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(UserRoutes)
  ],
  declarations: [UserComponent]
})
export class UserModule { }