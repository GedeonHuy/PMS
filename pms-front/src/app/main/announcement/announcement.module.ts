import { AnnouncementComponent } from './announcement.component';
import { PaginationModule } from './../../shared/pagination/pagination.module';
import { FormsModule } from '@angular/forms';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { ModalModule } from 'ngx-bootstrap/modal';

const AnnouncementRoutes: Routes = [
  //localhost:xxxx/main/user
  { path: '', redirectTo: 'index', pathMatch: 'full' },
  //localhost:xxxx/main/user/index
  { path: 'index', component: AnnouncementComponent }
]
@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    PaginationModule,
    ModalModule.forRoot(),
    RouterModule.forChild(AnnouncementRoutes)
  ],
  declarations: [AnnouncementComponent],
  providers: [DataService, NotificationService]
})
export class AnnouncementModule { }
