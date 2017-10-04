import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RoleComponent } from './role.component';
import { Routes, RouterModule } from '@angular/router';

const RoleRoutes: Routes = [
  //localhost:xxxx/main/user
  { path: '', redirectTo: 'index', pathMatch: 'full' },
  //localhost:xxxx/main/user/index
  { path: 'index', component: RoleComponent }
]

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(RoleRoutes)
  ],
  declarations: [RoleComponent],
  providers: [DataService, NotificationService]
})
export class RoleModule { }
