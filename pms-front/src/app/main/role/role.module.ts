import { FormsModule } from '@angular/forms';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RoleComponent } from './role.component';
import { Routes, RouterModule } from '@angular/router';
import {ModalModule } from 'ngx-bootstrap/modal';

const RoleRoutes: Routes = [
  //localhost:xxxx/main/user
  { path: '', redirectTo: 'index', pathMatch: 'full' },
  //localhost:xxxx/main/user/index
  { path: 'index', component: RoleComponent }
]

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    FormsModule,
    ModalModule.forRoot(),
    RouterModule.forChild(RoleRoutes)
  ],
  declarations: [RoleComponent],
  providers: [DataService, NotificationService]
})
export class RoleModule { }
