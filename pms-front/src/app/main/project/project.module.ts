import { PaginationModule } from './../../shared/pagination/pagination.module';
import { FormsModule } from '@angular/forms';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ProjectComponent } from './project.component';

const ProjectRoutes: Routes = [
  //localhost:xxxx/main/user
  { path: '', redirectTo: 'index', pathMatch: 'full' },
  //localhost:xxxx/main/user/index
  { path: 'index', component: ProjectComponent }
]

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    PaginationModule,
    ModalModule.forRoot(),
    RouterModule.forChild(ProjectRoutes)
  ],
  declarations: [ProjectComponent],
  providers: [DataService, NotificationService]
})
export class ProjectModule { }
