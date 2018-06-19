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
  isGuest: boolean;
  isAdmin: boolean;
  isLecturer: boolean;
  isStudent: boolean;
  isLoadGroupInBoard: boolean;
  isLoadData: boolean;
  isLoadLecturerCouncil : boolean;
  isLoadGroupByAdmin: boolean;
  isLoadGroupForGuest: boolean;
  isLoadGroupByLecturer: boolean;
  isLoadGroupAccepted: boolean;   
  thisLecturerId: any;
  groupByAdmin : any;
  groupForGuest: any;
  constructor(private router: Router, private _authenService: AuthenService, private _dataService: DataService, private _notificationService: NotificationService) {
    this.isSaved = false;
    this.isGuest = false;
    this.isAdmin = false;
    this.isStudent = false;
    this.isLecturer = false;
    this.isLoadData = false;
    this.isLoadLecturerCouncil = false;
  }
  element: any;
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
  public groupsInBoard = [];
  
  ngOnInit() {
    this.today = Date.now();


    this.user = this._authenService.getLoggedInUser();
    this.permissionAccess();

    if (this.user.role === "Guest") {
      this.loadGuestData();
    }

    if (this.user.role === "Student") {
      this.loadStudentGroup();
    }

    if (this.user.role === "Lecturer") {
      Observable.forkJoin([
        this._dataService.get("/api/lecturers/getall"),
  
      ]).subscribe(data => {
        this.lecturers = data[0].items,
        this.isLoadData = true;
      });

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

      this.loadGroupByAdmin('All');
    }

  }

  loadGuestData() {
    this.loadGroupForGuest();
    this.isLoadData = true;
  }

  loadLecturerData() {
    this.loadLecturerGroup();
    this.loadLecturerGroupAccepted();
    this.loadLecturerGroupInBoard();
    this.isLoadData = true;
  }

  loadLecturerGroup() {
    this._dataService.get("/api/lecturers/getgroups/" + this.user.email + "?isConfirm=Pending&pageSize=3").subscribe((response: any) => {
      this.groups = response;
      this.isLoadGroupByLecturer = true;      
    });
  }

  loadLecturerGroupAccepted() {
    this._dataService.get("/api/lecturers/getgroups/" + this.user.email + "?isConfirm=Accepted&pageSize=3").subscribe((response: any) => {
      this.groupsAccepted = response;
      this.isLoadGroupAccepted = true;        
    });
  }

  loadLecturerGroupInBoard() {
    
    Observable.forkJoin([
      this._dataService.get("/api/groups/getgroupsbylectureremailinboard/" + this.user.email),  
    ]).subscribe(data => {
      this.thisLecturerId = this.lecturers.find(l => l.email === this.user.email).lecturerId;
      console.log(this.thisLecturerId);
      for (var i=0; i<data[0].items.length; i++) {
        if (data[0].items[i].board.lecturerInformations.chair.lecturerId
          == this.thisLecturerId) {
            this.element = {
              isGraded: false,
              group: data[0].items[i],
            };
            var graded = false;
            if (data[0].items[i].board.lecturerInformations.chair.score != null) {
              graded = true;
            }
            this.element.isGraded = graded;
            this.groupsInBoard.push(this.element);
        } else if (data[0].items[i].board.lecturerInformations.secretary.lecturerId
          == this.thisLecturerId) {
            this.element = {
              isGraded: false,
              group: data[0].items[i],
            };
            var graded = false;
            if (data[0].items[i].board.lecturerInformations.secretary.score != null) {
              graded = true;
            }
            this.element.isGraded = graded;
            this.groupsInBoard.push(this.element);
        } else if (data[0].items[i].board.lecturerInformations.supervisor.lecturerId
          == this.thisLecturerId) {
            this.element = {
              isGraded: false,
              group: data[0].items[i],
            };
            var graded = false;
            if (data[0].items[i].board.lecturerInformations.supervisor.score != null) {
              graded = true;
            }
            this.element.isGraded = graded;
            this.groupsInBoard.push(this.element);
        } else if (data[0].items[i].board.lecturerInformations.reviewer.lecturerId
          == this.thisLecturerId) {
            this.element = {
              isGraded: false,
              group: data[0].items[i],
            };
            var graded = false;
            if (data[0].items[i].board.lecturerInformations.reviewer.score != null) {
              graded = true;
            }
            this.element.isGraded = graded;
            this.groupsInBoard.push(this.element);
        }
      }
      this.isLoadGroupInBoard = true;  
    });
  }

  loadStudentGroup() {
    this._dataService.get("/api/students/getgroups/" + this.user.email + "?pageSize=3").subscribe((response: any) => {
      this.groupsStudent = response;
      this.isLoadData = true;      
    });
  }

  loadGroupForGuest() {
    this._dataService.get("/api/groups/getall").subscribe((response: any) => {
      this.groupForGuest = response;
      this.isLoadGroupForGuest = true;
      this.groupForGuest.items = this.groupForGuest.items.filter(g => g.isConfirm === 'Accepted');
      console.log(this.groupForGuest.items);
    }); 
  }

  loadGroupByAdmin(y) {
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
    if (this.user.role === "Guest") {
      this.isGuest = true;
    }
  }
}
