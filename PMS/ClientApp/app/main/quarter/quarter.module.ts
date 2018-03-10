import { QuarterComponent } from './quarter.component';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { routing } from './quarter.routing';
import { NgaModule } from '../../theme/nga.module';
import { Daterangepicker } from 'ng2-daterangepicker';

@NgModule({
  imports: [
    NgaModule,
    ModalModule.forRoot(),
    CommonModule,
    Daterangepicker,    
    FormsModule,
    routing
  ],
  declarations: [
    QuarterComponent
  ],
  providers: [DataService, NotificationService]
})
export class QuarterModule { }
