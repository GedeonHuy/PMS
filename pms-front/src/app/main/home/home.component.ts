import { Router } from '@angular/router';
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
  @ViewChild('gradingModal') public gradingModal: ModalDirective;
  public enrollment: any;
  public enrollments: any = {};
  public enrollmentsAccept: any = {};
  public groups: any = {};
  public groupsStudent: any = {};
  public groupsAccepted: any = {};
  public user: any;

  councilEnrollments: any[];

  public isClicked: boolean;
  public isLoading: boolean;

  isAdmin: boolean;
  isLecturer: boolean;
  isStudent: boolean;
  isLoadData: boolean;

  constructor(private router: Router, private _authenService: AuthenService, private _dataService: DataService, private _notificationService: NotificationService) {
    this.isLoading = false;
    this.isClicked = false;
    this.isAdmin = false;
    this.isStudent = false;
    this.isLecturer = false;
    this.isLoadData = false;
  }
  projects: any[];
  lecturers: any[];
  quarters: any[];

  public totalProjects: any;
  public totalLecturers: any;
  public totalStudents: any;

  public types: any[] = [ProjectTypesConstants.A, ProjectTypesConstants.B, ProjectTypesConstants.C, ProjectTypesConstants.D];


  ngOnInit() {
    Observable.forkJoin([
      this._dataService.get("/api/quarters/getall"),
      this._dataService.get("/api/projects/getall"),
      this._dataService.get("/api/lecturers/getall"),
      this._dataService.get("/api/students/getall")

    ]).subscribe(data => {
      this.quarters = data[0].items,
        this.totalProjects = data[1].totalItems,
        this.lecturers = data[2].items,
        this.totalLecturers = data[2].totalItems,
        this.totalStudents = data[3].totalItems,
      this.isLoading = true;
    });
    this.user = this._authenService.getLoggedInUser();
    this.permissionAccess();

    if (this.user.role === "Student") {
      this.loadDataStudent();
    }

    if (this.user.role === "Lecturer") {
      this.loadLecturerData();
    }
  }

  loadDataStudent() {
    this.loadStudentEnrollment();
    this.loadStudentGroup();
  }

  loadStudentEnrollment() {
    this._dataService.get("/api/students/getenrollments/" + this.user.email + "?pageSize=3").subscribe((response: any) => {
      this.enrollments = response;
      this.isLoadData = true;      
    });
  }

  loadStudentGroup() {
    this._dataService.get("/api/students/getgroups/" + this.user.email + "?pageSize=3").subscribe((response: any) => {
      this.groupsStudent = response;
      this.isLoadData = true;      
    });
  }

  loadLecturerData() {
    this.loadLecturerEnrollment();
    this.loadLecturerEnrollmentAccepted();
    this.loadLecturerGroup();
    this.loadLecturerGroupAccepted();
    this.loadLecturerCouncilEnrollments();
  }

  loadLecturerCouncilEnrollments() {
    this._dataService.get("/api/councilenrollments/getcouncilenrollmentsbylectureremail/" + this.user.email + "?pageSize=3").subscribe((response: any) => {
      this.councilEnrollments = response.items;
      this.isLoadData = true;      
    });
  }

  loadLecturerEnrollment() {
    this._dataService.get("/api/lecturers/getenrollments/" + this.user.email + "?isConfirm=Pending&pageSize=3").subscribe((response: any) => {
      this.enrollments = response;
      this.isLoadData = true;      
    });
  }

  loadLecturerEnrollmentAccepted() {
    this._dataService.get("/api/lecturers/getenrollments/" + this.user.email + "?isConfirm=Accepted&pageSize=3").subscribe((response: any) => {
      this.enrollmentsAccept = response;
      this.isLoadData = true;      
    });
  }

  loadLecturerGroup() {
    this._dataService.get("/api/lecturers/getgroups/" + this.user.email + "?isConfirm=Pending&pageSize=3").subscribe((response: any) => {
      this.groups = response;
      this.isLoadData = true;      
    });
  }

  loadLecturerGroupAccepted() {
    this._dataService.get("/api/lecturers/getgroups/" + this.user.email + "?isConfirm=Accepted&pageSize=3").subscribe((response: any) => {
      this.groupsAccepted = response;
      this.isLoadData = true;      
    });
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
        this._dataService.post('/api/enrollments/add', JSON.stringify(this.enrollment))
          .subscribe((response: any) => {
            this.enrollmentModal.hide();
            this.loadStudentEnrollment();
            this._notificationService.printSuccessMessage("Add Success");
            this.isClicked = false;
            this.isLoading = false;
          }, error => this._dataService.handleError(error));
      }
    }
  }

  permissionAccess() {
    if (this.user.role === "Admin") {
      this.isAdmin = true;
    }
    if (this.user.role === "Lecturer") {
      this.isLecturer = true;
    }
    if (this.user.role === "Student") {
      this.isStudent = true;
    }
  }
}
