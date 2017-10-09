import { FormsModule } from '@angular/forms';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { ModalModule } from 'ngx-bootstrap/modal';
import { StudentComponent } from './student.component';

const StudentRoutes: Routes = [
  //localhost:xxxx/main/user
  { path: '', redirectTo: 'index', pathMatch: 'full' },
  //localhost:xxxx/main/user/index
  { path: 'index', component: StudentComponent }
]

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ModalModule.forRoot(),
    RouterModule.forChild(StudentRoutes)
  ],
  declarations: [StudentComponent],
  providers: [DataService, NotificationService]
})
export class StudentModule { }
