import { NotificationService } from './../../core/services/notification.service';
import { PaginationModule } from './../../core/pagination/pagination.module';
import { DataService } from './../../core/services/data.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { RoleComponent } from './role.component';
import { routing } from './role.routing';
import { NgaModule } from '../../theme/nga.module';

@NgModule({
  imports: [
    NgaModule,
    ModalModule.forRoot(),
    CommonModule,
    FormsModule,
    PaginationModule,
    routing
  ],
  declarations: [

    RoleComponent
  ],
  providers: [DataService, NotificationService]
})
export class RoleModule { }
