import { PaginationModule } from './../../shared/pagination/pagination.module';
import { LecturerComponent } from './lecturer.component';
import { FormsModule } from '@angular/forms';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { ModalModule } from 'ngx-bootstrap/modal';

const LecturerRoutes: Routes = [
  //localhost:xxxx/main/lecturer
  { path: '', redirectTo: 'index', pathMatch: 'full' },
  //localhost:xxxx/main/lecturer/index
  { path: 'index', component: LecturerComponent }
]

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    PaginationModule,
    ModalModule.forRoot(),
    RouterModule.forChild(LecturerRoutes)
  ],
  declarations: [LecturerComponent],
  providers: [DataService, NotificationService]
})
export class LecturerModule { }
