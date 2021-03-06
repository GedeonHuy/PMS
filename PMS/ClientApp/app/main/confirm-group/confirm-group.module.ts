import { PaginationModule } from './../../core/pagination/pagination.module';
import { GroupConfirmComponent } from './confirm-group.component';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { routing } from './confirm-group.routing';
import { NgaModule } from '../../theme/nga.module';
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';
@NgModule({
  imports: [
    NgaModule,
    ModalModule.forRoot(),
    CommonModule,
    PaginationModule,
    MultiselectDropdownModule,
    FormsModule,
    routing
  ],
  declarations: [
    GroupConfirmComponent
  ],
  providers: [DataService, NotificationService]
})
export class GroupConfirmModule { }
