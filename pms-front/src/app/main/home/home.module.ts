import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { ModalModule } from 'ngx-bootstrap/modal';

const HomeRoutes: Routes = [
  //localhost:4200/main/home
  { path: '', redirectTo: 'index', pathMatch: 'full' },
  //localhost:4200/main/home/index
  { path: 'index', component: HomeComponent }
]
@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ModalModule.forRoot(),
    RouterModule.forChild(HomeRoutes)
  ],
  declarations: [HomeComponent],
  providers: [DataService, NotificationService]
})
export class HomeModule { }