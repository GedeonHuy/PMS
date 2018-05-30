import { PaginationModule } from './../../core/pagination/pagination.module';

import { DataService } from './../../core/services/data.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ProjectComponent } from './project.component';
import { routing } from './project.routing';
import { NgaModule } from '../../theme/nga.module';
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';
import { NotificationService } from '../../core/services/notification.service';
@NgModule({
  imports: [
    NgaModule,
    ModalModule.forRoot(),
    CommonModule,
    FormsModule,
    PaginationModule,
    MultiselectDropdownModule,
    routing
  ],
  declarations: [
    ProjectComponent
  ],
  providers: [DataService, NotificationService]
})
export class ProjectModule { }
