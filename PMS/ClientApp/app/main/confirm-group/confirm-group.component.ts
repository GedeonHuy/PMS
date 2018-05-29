import { Observable } from 'rxjs/Observable';
import "rxjs/add/observable/forkJoin";

import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AuthenService } from './../../core/services/authen.service';
import { IMultiSelectOption, IMultiSelectSettings } from 'angular-2-dropdown-multiselect';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-group',
  templateUrl: './confirm-group.component.html'
})

export class GroupConfirmComponent implements OnInit {
  @ViewChild('modalAddEdit') public modalAddEdit: ModalDirective;
  @ViewChild('modalMark') public modalMark: ModalDirective;
  
  public groups: any[];
  public queryResult: any = {};

  public group: any = {};

  public isSaved: boolean;
  public isLoadBoard: boolean;
  public isLoadData: boolean;
  public isLoadMark: boolean;

  public boardEnrollments: any;
  public boardEnrollment: any;
  public boardEnrollmentJson: any = {};
  public board: any = {};
  public comment: any;
  public chair: any;
  public secretary: any;
  public supervisor: any;
  public reviewer: any;

  public scorePercents: number[] = [10, 20, 25, 30, 50, 75, 100];

  isAdmin: boolean;
  isLecturer: boolean;
  isStudent: boolean;

  thisLecturerEmail: any;

  projects: any[];
  lecturers: any[];
  quarters: any[];
  boardEnrollmentsOfLecturer: any[];

  majors: any[];
  PAGE_SIZE = 5;

  query: any = {
    pageSize: this.PAGE_SIZE,
    isConfirm: "Accepted"
  };

  public user: any;

  public typeStatus: any[] = ["Accepted", "Pending", "Denied"];

  constructor(private _authenService: AuthenService, private _dataService: DataService, private _notificationService: NotificationService) {
    this.isLoadData = false;
    this.isSaved = false;
    this.isAdmin = false;
    this.isLecturer = false;
    this.isLoadMark = false;
  }

  ngOnInit() {
    this.loadData();
    this.permissionAccess();

    Observable.forkJoin([
      this._dataService.get("/api/lecturers/getall/")
    ]).subscribe(data => {
      this.lecturers = data[0].items
    });
  }

  loadData() {
    this._dataService.get("/api/groups/getall" + "?" + this.toQueryString(this.query)).subscribe((response: any) => {
      this.queryResult = response;
      this.isLoadData = true;
    });
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

  //Create method
  assignBoard(id: any) {
    this.modalAddEdit.show();
    this.boardEnrollments = {};
    this.chair = {};
    this.secretary = {};
    this.supervisor = {};
    this.reviewer = {};

    Observable.forkJoin(
      this._dataService.get('/api/groups/getgroup/' + id)
    ).subscribe(data => {
      this.group = data[0];

      if (this.group.board == null) {
        this.group.board = {};

        //this.lecturers = this.lecturers.filter(l => l.majorId == this.group.majorId);
        this.board.groupId = this.group.groupId;
        this.board.lecturerInformations = this.boardEnrollments;
        this.board.lecturerInformations.chair = this.chair;
        this.board.lecturerInformations.secretary = this.secretary;
        this.board.lecturerInformations.supervisor = this.supervisor;
        this.board.lecturerInformations.reviewer = this.reviewer;

        this.isLoadBoard = true;
      } else {

        this.board.groupId = this.group.groupId;
        this.chair = {
          name: this.lecturers.find(l => l.lecturerId == this.group.board.lecturerInformations.chair.lecturerId).name,
          lecturerId: this.group.board.lecturerInformations.chair.lecturerId,
          scorePercent: this.group.board.lecturerInformations.chair.scorePercent
        };
        this.secretary = {
          name: this.lecturers.find(l => l.lecturerId == this.group.board.lecturerInformations.secretary.lecturerId).name,
          lecturerId: this.group.board.lecturerInformations.secretary.lecturerId,
          scorePercent: this.group.board.lecturerInformations.secretary.scorePercent
        };
        this.supervisor = {
          name: this.lecturers.find(l => l.lecturerId == this.group.board.lecturerInformations.supervisor.lecturerId).name,
          lecturerId: this.group.board.lecturerInformations.supervisor.lecturerId,
          scorePercent: this.group.board.lecturerInformations.supervisor.scorePercent
        };
        this.reviewer = {
          name: this.lecturers.find(l => l.lecturerId == this.group.board.lecturerInformations.reviewer.lecturerId).name,
          lecturerId: this.group.board.lecturerInformations.reviewer.lecturerId,
          scorePercent: this.group.board.lecturerInformations.reviewer.scorePercent
        };
        
        this.boardEnrollments = {
          chair: this.chair,
          secretary: this.secretary,
          supervisor: this.supervisor,
          reviewer: this.reviewer
        };
        this.board.lecturerInformations = this.boardEnrollments;

        this.isLoadBoard = true;
      }
    });
  }

  //Mark method
  mark(id: any) {
    this.modalMark.show();

    Observable.forkJoin(
      this._dataService.get('/api/boardenrollments/getboardenrollmentsbylectureremail/' + this.thisLecturerEmail)
    ).subscribe(data => {
      this.boardEnrollmentsOfLecturer = data[0].items;
      console.log(this.boardEnrollmentsOfLecturer);
      this.boardEnrollment = this.boardEnrollmentsOfLecturer.find(be => be.boardID == id);
      this.isLoadMark = true;
    });
  }

  handler(type: string, $event: ModalDirective) {
    if (type === "onHide" || type === "onHidden") {
      this.group = [];
      this.isLoadBoard = false;
    }
  }

  hidemodalAddEdit() {
    this.modalAddEdit.hide();
  }

  hidemodalMark() {
    this.modalMark.hide();
  }
  
  saveChange(form: NgForm) {
    if (form.valid) {
      this.isSaved = true;

      if (this.board.boardId == undefined) {
        this._dataService.post('/api/boards/add', JSON.stringify(this.board))
          .subscribe((response: any) => {
            this.modalAddEdit.hide();
            this.loadData();
            this._notificationService.printSuccessMessage("Add Success");
            form.resetForm();

            this.isSaved = false;
            this.isLoadData =false;
          }, error => this._dataService.handleError(error));
      }
      else {
        this._dataService.put('/api/boards/update/' + this.board.boardId, JSON.stringify(this.board))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            this._notificationService.printSuccessMessage("Update Success");
            form.resetForm();

            this.isSaved = false;
            this.isLoadData =false;
          }, error => this._dataService.handleError(error));
      }
    }
  }

  saveMark(form: NgForm) {
    if (form.valid) {
      this.isSaved = true;

      if (this.boardEnrollment.score != null)
      { 
        //make legit json boardEnrollment
        // this.boardEnrollmentJson = {
        //   boardEnrollmentId: this.boardEnrollment.boardEnrollmentId,
        //   score: this.boardEnrollment.score,
        //   percentage: this.boardEnrollment.percentage,
        //   lecturerId: this.boardEnrollment.lecturerId,
        //   boardID: this.boardEnrollment.boardId
        // };

        this._dataService.put('/api/boardenrollments/update/' + this.boardEnrollment.boardEnrollmentId, JSON.stringify(this.boardEnrollment))
          .subscribe((response: any) => {
            this.loadData();
            this.modalMark.hide();
            this._notificationService.printSuccessMessage("Update Success");
            this.isSaved = false;
            this.isLoadData = false;
            this.isLoadMark = false;
          }, error => this._dataService.handleError(error));
      }
    }
  }

  permissionAccess() {
    this.user = this._authenService.getLoggedInUser();
    if (this.user.role === "Admin") {
      this.isAdmin = true;
    }

    if (this.user.role === "Lecturer") {
      this.isLecturer = true;
      this.thisLecturerEmail = this.user.email;
    }

    if (this.user.role === "Student") {
      this.isStudent = true;
    }
  }

  onPageChange(page) {
    this.query.page = page;
    this.loadData();
  }


}