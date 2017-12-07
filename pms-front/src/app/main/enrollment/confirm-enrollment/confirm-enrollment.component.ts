import { SystemConstants } from './../../../core/common/system.constants';
import { query } from '@angular/core/src/animation/dsl';
import { AuthenService } from './../../../core/services/authen.service';
import { NotificationService } from './../../../core/services/notification.service';
import { DataService } from './../../../core/services/data.service';
import { Response } from '@angular/http';
import { Component, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-confirm-enrollment',
  templateUrl: './confirm-enrollment.component.html',
  styleUrls: ['./confirm-enrollment.component.css']
})
export class ConfirmEnrollmentComponent implements OnInit {

  public enrollments: any[];
  public queryResult: any = {};

  public enrollment: any;
  public isClicked: boolean;
  public isLoading: boolean;
  public user: any;
  isAdmin: boolean;
  isLecturer: boolean;
  isLoadData: boolean;

  queryAdmin: any = {
    pageSize: SystemConstants.PAGE_SIZE,
    isConfirm: "Accepted"
  };

  query: any = {
    pageSize: SystemConstants.PAGE_SIZE,
    isConfirm: "Accepted"
  };

  public typeStatus: any[] = ["Accepted", "Pending", "Denied"];


  constructor(private _authenService: AuthenService, private _dataService: DataService, private _notificationService: NotificationService) {
    this.isClicked = false;
    this.isAdmin = false;
    this.isLecturer = false;
    this.isLoading = false;
    this.isLoadData = false;
  }

  ngOnInit() {
    this.user = this._authenService.getLoggedInUser();
    if (this.user.role === "Admin") {
      this.loadDataAdmin();
    }

    if (this.user.role === "Lecturer") {
      this.loadData();
    }
  }

  loadDataAdmin() {
    this._dataService.get("/api/enrollments/getall" + "?" + this.toQueryString(this.queryAdmin)).subscribe((response: any) => {
      this.queryResult = response;
      this.isLoadData = true;
    });
  }

  loadData() {
    this._dataService.get("/api/lecturers/getenrollments/" + this.user.email + "?" + this.toQueryString(this.query)).subscribe((response: any) => {
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

  onPageChange(page) {
    this.isLoadData = false;
    this.query.page = page;
    if (this.user.role === "Admin") {
      this.loadDataAdmin();
    }

    if (this.user.role === "Lecturer") {
      this.loadData();
    }
  }
}
