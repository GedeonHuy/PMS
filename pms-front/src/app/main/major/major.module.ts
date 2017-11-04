import { MajorComponent } from './major.component';
import { PaginationModule } from './../../shared/pagination/pagination.module';
import { FormsModule } from '@angular/forms';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { ModalModule } from 'ngx-bootstrap/modal';

const MajorRoutes: Routes = [
  //localhost:xxxx/main/lecturer
  { path: '', redirectTo: 'index', pathMatch: 'full' },
  //localhost:xxxx/main/lecturer/index
  { path: 'index', component: MajorComponent }
]

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    PaginationModule,
    ModalModule.forRoot(),
    RouterModule.forChild(MajorRoutes)
  ],
  declarations: [MajorComponent],
  providers: [DataService, NotificationService]
})
export class MajorModule { }
