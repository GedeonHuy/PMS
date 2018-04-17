import { ProgressService } from './../core/services/progress.service';
import { AdminLecturerAuthGuard } from './../core/guards/auth-admin-lecturer.guard';
import { StudentAuthGuard } from './../core/guards/auth-student.guard';
import { LecturerAuthGuard } from './../core/guards/auth-lecturer.guard';
import { AdminAuthGuard } from './../core/guards/auth-admin.guard';
import { UtilityService } from './../core/services/utility.service';
import { AuthenService } from './../core/services/authen.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { routing } from './main.routing';
import { NgaModule } from '../theme/nga.module';
import { AppTranslationModule } from '../app.translation.module';
import { Main } from './main.component';


@NgModule({
  imports: [CommonModule, AppTranslationModule, NgaModule, routing],
  declarations: [Main],
  providers: [ProgressService, UtilityService,
    AuthenService, AdminAuthGuard,
    LecturerAuthGuard, AdminLecturerAuthGuard, StudentAuthGuard]

})
export class MainModule {
}
