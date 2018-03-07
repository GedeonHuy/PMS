import { NotificationService } from 'app/core/services/notification.service';
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
    routing
  ],
  declarations: [

    RoleComponent
  ],
  providers: [DataService, NotificationService]
})
export class RoleModule { }
