import { ConfirmEnrollmentComponent } from './confirm-enrollment/confirm-enrollment.component';
import { EnrollmentComponent } from './enrollment.component';
import { PaginationModule } from './../../shared/pagination/pagination.module';
import { FormsModule } from '@angular/forms';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { ModalModule } from 'ngx-bootstrap/modal';

const LecturerRoutes: Routes = [
  //localhost:xxxx/main/enrollment
  { path: '', redirectTo: 'index', pathMatch: 'full' },
  //localhost:xxxx/main/enrollment/index
  { path: 'pending', component: EnrollmentComponent },
  //localhost:xxxx/main/enrollment/index
  { path: 'accepted', component: ConfirmEnrollmentComponent }
]

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    PaginationModule,
    ModalModule.forRoot(),
    RouterModule.forChild(LecturerRoutes)
  ],
  declarations: [EnrollmentComponent, ConfirmEnrollmentComponent],
  providers: [DataService, NotificationService]
})
export class EnrollmentModule { }
