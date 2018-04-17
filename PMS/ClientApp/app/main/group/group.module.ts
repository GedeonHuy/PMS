import { GroupComponent } from './group.component';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { routing } from './group.routing';
import { NgaModule } from '../../theme/nga.module';
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';

@NgModule({
  imports: [
    NgaModule,
    ModalModule.forRoot(),
    CommonModule,
    MultiselectDropdownModule,
    FormsModule,
    routing
  ],
  declarations: [
    GroupComponent
  ],
  providers: [DataService, NotificationService]
})
export class GroupModule { }
