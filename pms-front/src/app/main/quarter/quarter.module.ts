import { QuarterComponent } from './quarter.component';
import { PaginationModule } from './../../shared/pagination/pagination.module';
import { FormsModule } from '@angular/forms';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { ModalModule } from 'ngx-bootstrap/modal';
import { Daterangepicker } from 'ng2-daterangepicker';


const QuarterRoutes: Routes = [
  //localhost:xxxx/main/user
  { path: '', redirectTo: 'index', pathMatch: 'full' },
  //localhost:xxxx/main/user/index
  { path: 'index', component: QuarterComponent }
]
@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    PaginationModule,
    Daterangepicker,    
    ModalModule.forRoot(),
    RouterModule.forChild(QuarterRoutes)
  ],
  declarations: [QuarterComponent],
  providers: [DataService, NotificationService]
})
export class QuarterModule { }
