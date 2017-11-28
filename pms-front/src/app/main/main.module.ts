import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainComponent } from './main.component';
import { MainRoutes } from './main.routes';
import { RouterModule, Routes } from '@angular/router';


import { UserModule } from './user/user.module';
import { HomeModule } from './home/home.module';
import { RoleModule } from './role/role.module';
import { StudentModule } from './student/student.module';
import { LecturerModule } from './lecturer/lecturer.module';
import { ProjectModule } from './project/project.module';
import { GroupModule } from './group/group.module';
import { MajorModule } from './major/major.module';
import { EnrollmentModule } from './enrollment/enrollment.module';
import { ConfirmGroupModule } from './confirm-group/confirm-group.module';
import { PaginationModule } from './../shared/pagination/pagination.module';
import { GroupDetailsComponent } from './group-details/group-details.component';

import { SidebarMenuComponent } from './../shared/sidebar-menu/sidebar-menu.component';
import { TopMenuComponent } from './../shared/top-menu/top-menu.component';

import { UtilityService } from './../core/services/utility.service';
import { AuthenService } from './../core/services/authen.service';
import { AdminAuthGuard } from './../core/guards/auth-admin.guard';
import { SignalrService } from './../core/services/signalr.service';
import { LecturerAuthGuard } from './../core/guards/auth-lecturer.guard';
import { StudentAuthGuard } from './../core/guards/auth-student.guard';
import { GradeComponent } from './grade/grade.component';
import { AdminLecturerAuthGuard } from './../core/guards/auth-admin-lecturer.guard';
<<<<<<< HEAD
import { GroupDetailsModule } from './group-details/group-details.module';
=======
import { CouncilModule } from './council/council.module';
>>>>>>> ff833f5cfe74fe60b4c3f1c3f1fadf1f4a7ac1cc

@NgModule({
  imports: [
    CommonModule,
    HomeModule,    
    UserModule,
    RoleModule,
    StudentModule,
    LecturerModule,
    ProjectModule,
    MajorModule,
    EnrollmentModule,
    GroupModule,
<<<<<<< HEAD
    GroupDetailsModule,    
=======
    CouncilModule,    
>>>>>>> ff833f5cfe74fe60b4c3f1c3f1fadf1f4a7ac1cc
    ConfirmGroupModule,
    PaginationModule,
    RouterModule.forChild(MainRoutes)
  ],
  declarations: [MainComponent, SidebarMenuComponent, TopMenuComponent],
  providers: [UtilityService, AuthenService, AdminAuthGuard, LecturerAuthGuard, AdminLecturerAuthGuard, StudentAuthGuard, SignalrService]
})
export class MainModule { }