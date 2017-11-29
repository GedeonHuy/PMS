import { GroupDetailsComponent } from './group-details.component';
import { FormsModule } from '@angular/forms';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ChartModule } from 'angular2-chartjs';

const GroupDetailsRoutes: Routes = [
  //localhost:xxxx/main/user/index
  { path: '', component: GroupDetailsComponent }
]

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ChartModule,
    ModalModule.forRoot(),
    RouterModule.forChild(GroupDetailsRoutes)
  ],
  declarations: [GroupDetailsComponent],
  providers: [DataService, NotificationService]
})

export class GroupDetailsModule { }
