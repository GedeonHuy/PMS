import { DataService } from './../../core/services/data.service';

import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AuthenService } from './../../core/services/authen.service';
import { ProjectTypesConstants } from './../../core/common/projectType.constants';
import "rxjs/add/observable/forkJoin";
@Component({
  selector: 'dashboard',
  styleUrls: ['./dashboard.scss'],
  templateUrl: './dashboard.html'
})
export class Dashboard {
  public isSaved: boolean;
  public user: any;
  public today: any;
  isAdmin: boolean;
  isLecturer: boolean;
  isStudent: boolean;
  isLoadData: boolean;
  isLoadLecturerCouncil : boolean;
  groupByAdmin : any;
  isLoadGroupByAdmin: boolean;
  constructor(private router: Router, private _authenService: AuthenService, private _dataService: DataService, private _notificationService: NotificationService) {
    this.isSaved = false;
    this.isAdmin = false;
    this.isStudent = false;
    this.isLecturer = false;
    this.isLoadData = false;
    this.isLoadLecturerCouncil = false;
  }
  projects: any[];
  lecturers: any[];
  quarters: any[];
  councilEnrollments: any[];
  public thisCouncilEnrollment: any;

  tmp: any;
  public totalProjects: any;
  public totalLecturers: any;
  public totalStudents: any;

  public types: any[] = [ProjectTypesConstants.A, ProjectTypesConstants.B, ProjectTypesConstants.C, ProjectTypesConstants.D];

  public groupsAccepted: any = {};
  public groupsStudent: any = {};
  public groups: any = {};
  public groupsInBoard = {};
  
  ngOnInit() {
    this.today = Date.now();


    this.user = this._authenService.getLoggedInUser();
    this.permissionAccess();

    if (this.user.role === "Student") {
      this.loadStudentGroup();
    }

    if (this.user.role === "Lecturer") {
      this.loadLecturerData();
    }

    if(this.user.role === "Admin") {

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
        this.isLoadData = true;
      });

      this.loadGroupByAdmin();
    }

  }

  loadLecturerData() {
    this.loadLecturerGroup();
    this.loadLecturerGroupAccepted();
    this.loadLecturerGroupInBoard();
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

  loadLecturerGroupInBoard() {
    this._dataService.get("/api/groups/getgroupsbylectureremailinboard/" + this.user.email).subscribe((response: any) => {
      this.groupsInBoard = response;
      this.isLoadData = true;      
    });
  }

  loadStudentGroup() {
    this._dataService.get("/api/students/getgroups/" + this.user.email + "?pageSize=3").subscribe((response: any) => {
      this.groupsStudent = response;
      this.isLoadData = true;      
    });
  }



  loadGroupByAdmin() {

    this._dataService.get("/api/groups/getall").subscribe((response: any) => {
      this.groupByAdmin = response;
      this.isLoadGroupByAdmin = true;  
    });

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
