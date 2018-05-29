import { NgModule }      from '@angular/core';
import { CommonModule }  from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AppTranslationModule } from '../../app.translation.module';
import { NgaModule } from '../../theme/nga.module';

import { Dashboard } from './dashboard.component';
import { routing }       from './dashboard.routing';

import { Calendar } from './calendar';
import { CalendarService } from './calendar/calendar.service';

import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    AppTranslationModule,
    NgaModule,
    routing
  ],
  declarations: [
    Calendar,
    Dashboard
  ],
  providers: [
    CalendarService,
    DataService, NotificationService
  ]
})
export class DashboardModule {}
