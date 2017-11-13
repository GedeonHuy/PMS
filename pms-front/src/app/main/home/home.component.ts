import { Observable } from 'rxjs/Observable';
import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AuthenService } from './../../core/services/authen.service';
import { ProjectTypesConstants } from './../../core/common/projectType.constants';

import "rxjs/add/Observable/forkJoin";


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  @ViewChild('enrollmentModal') public enrollmentModal: ModalDirective;
  public enrollment: any;

  public isClicked: boolean;
  public isLoading: boolean;


  isAdmin: boolean;
  isLecturer: boolean;
  isStudent: boolean;

  constructor(private _authenService: AuthenService, private _dataService: DataService, private _notificationService: NotificationService) {
    this.isLoading = false;
    this.isClicked = false;
    this.isAdmin = false;
    this.isStudent = false;
    this.isLecturer = false;
  }
  projects: any[];
  lecturers: any[];
  quarters: any[];

  public types: any[] = [ProjectTypesConstants.A, ProjectTypesConstants.B, ProjectTypesConstants.C, ProjectTypesConstants.D];


  ngOnInit() {
    Observable.forkJoin([
      this._dataService.get("/api/quarters/getall"),

      this._dataService.get("/api/lecturers/getall")
    ]).subscribe(data => {
      this.quarters = data[0].items,
      this.lecturers = data[1].items
      this.isLoading = true;
    });

    this.permissionAccess();
  }


  //Create method
  createEnrollment() {
    this.enrollmentModal.show();
    var user = this._authenService.getLoggedInUser();
    this.enrollment = {};
    this.enrollment.studentEmail = user.email;
    this.isLoading = true;
  }

  hideEnrollmentModal() {
    this.enrollmentModal.hide();
    this.isLoading = false;
  }

  toQueryString(obj) {
    var parts = [];
    for (var property in obj) {
      var value = obj[property];
      if (value != null && value != undefined)
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }
    return parts.join('&');
  }

  applyEnrollment(valid: boolean) {
    if (valid) {
      this.isClicked = true;      
      if (this.enrollment.id == undefined) {
        console.log(this.enrollment);
        this._dataService.post('/api/enrollments/add', JSON.stringify(this.enrollment))
          .subscribe((response: any) => {
            this.enrollmentModal.hide();
            this._notificationService.printSuccessMessage("Add Success");
            this.isClicked = false;
            this.isLoading = false;
          }, error => this._dataService.handleError(error));
      }
    }
  }

  permissionAccess() {
    var user = this._authenService.getLoggedInUser();
    if (user.role === "Admin" ) {
      this.isAdmin = true;
    }
    if (user.role === "Lecturer" ) {
      this.isLecturer = true;
    }
    if (user.role === "Student") {
      this.isStudent = true;
    }
  }
}
