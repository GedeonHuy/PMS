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

import { SidebarMenuComponent } from './../shared/sidebar-menu/sidebar-menu.component';
import { TopMenuComponent } from './../shared/top-menu/top-menu.component';

import { UtilityService } from './../core/services/utility.service';
import { AuthenService } from './../core/services/authen.service';
import { AdminAuthGuard } from './../core/guards/auth-admin.guard';

@NgModule({
  imports: [
    CommonModule,
    HomeModule,    
    UserModule,
    RoleModule,
    StudentModule,
    LecturerModule,
    ProjectModule,
    RouterModule.forChild(MainRoutes)
  ],
  declarations: [MainComponent, SidebarMenuComponent, TopMenuComponent],
  providers: [UtilityService, AuthenService, AdminAuthGuard]
})
export class MainModule { }