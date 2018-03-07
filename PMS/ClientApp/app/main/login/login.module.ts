import { NgModule }      from '@angular/core';
import { CommonModule }  from '@angular/common';
import { AppTranslationModule } from '../../app.translation.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgaModule } from '../../theme/nga.module';
import { Login } from './login.component';
import { routing }       from './login.routing';
import { AuthenService } from 'app/core/services/authen.service';
import { NotificationService } from 'app/core/services/notification.service';


@NgModule({
  imports: [
    CommonModule,
    AppTranslationModule,
    ReactiveFormsModule,
    FormsModule,
    NgaModule,
    routing
  ],
  providers: [AuthenService, NotificationService],
  declarations: [
    Login
  ]
})
export class LoginModule {}
