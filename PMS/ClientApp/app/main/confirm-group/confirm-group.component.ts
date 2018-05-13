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

  public groups: any[];
  public queryResult: any = {};

  public group: any = {};

  public isSaved: boolean;
  public isLoadBoard: boolean;
  public isLoadData: boolean;

  public boardEnrollments: any;
  public board: any = {};
  public chair: any;
  public secretary: any;
  public supervisor: any;
  public reviewer: any;

  public scorePercents: any[] = [25, 50, 75, 100];

  isAdmin: boolean;
  isLecturer: boolean;
  isStudent: boolean;

  projects: any[];
  lecturers: any[];
  quarters: any[];

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
  }

  ngOnInit() {
    this.loadData();
    this.permissionAccess();

    Observable.forkJoin([
      this._dataService.get("/api/lecturers/getall")
    ]).subscribe(data => {
      console.log(data[0].items);
      this.lecturers = data[0].items
    });
  }

  loadData() {
    this._dataService.get("/api/groups/getall" + "?" + this.toQueryString(this.query)).subscribe((response: any) => {
      this.queryResult = response;
      console.log(response);
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
        this.lecturers = this.lecturers.filter(l => l.majorId == this.group.majorId);
        this.isLoadBoard = true;
      }
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


  saveChange(form: NgForm) {
    if (form.valid) {
      this.isSaved = true;
      console.log(this.board);

      if (this.board.boardId == undefined) {
        this._dataService.post('/api/boards/add', JSON.stringify(this.board))
          .subscribe((response: any) => {
            this.modalAddEdit.hide();
            this.loadData();
            this._notificationService.printSuccessMessage("Add Success");
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
            this.isSaved = false;
            this.isLoadData =false;
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