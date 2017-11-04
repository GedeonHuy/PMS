import { ConfirmGroupComponent } from './confirm-group.component';
import { FormsModule } from '@angular/forms';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PaginationModule } from './../../shared/pagination/pagination.module';

const ConfirmGroupRoutes: Routes = [
  //localhost:xxxx/main/user
  { path: '', redirectTo: 'index', pathMatch: 'full' },
  //localhost:xxxx/main/council/index
  { path: 'index', component: ConfirmGroupComponent }
]

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    PaginationModule,
    ModalModule.forRoot(),
    RouterModule.forChild(ConfirmGroupRoutes)
  ],
  declarations: [ConfirmGroupComponent],
  providers: [DataService, NotificationService]
})
export class ConfirmGroupModule { }
