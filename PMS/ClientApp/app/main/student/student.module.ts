import { NgaModule } from './../../theme/nga.module';
import { NotificationService } from 'app/core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { StudentComponent } from './student.component';
import { routing } from './student.routing';
@NgModule({
  imports: [
    NgaModule,
    ModalModule.forRoot(),
    CommonModule,
    FormsModule,
    routing
  ],
  declarations: [
    StudentComponent
  ],
  providers: [DataService, NotificationService]
})
export class StudentModule { }
