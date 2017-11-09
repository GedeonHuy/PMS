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
  public isLoading : boolean;
  
  isAdmin: boolean;
  isLecturer: boolean;

  PAGE_SIZE = 3;

  query: any = {
    pageSize: this.PAGE_SIZE,
    isConfirm : "Accepted"
  };

  public typeStatus : any [] = ["Accepted", "Pending", "Denied"];
  

  constructor(private _authenService: AuthenService, private _dataService: DataService, private _notificationService: NotificationService) {
    this.isClicked = false;
    this.isAdmin = false;
    this.isLecturer = false;
    this.isLoading = false;
  }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this._dataService.get("/api/enrollments/getall" + "?" + this.toQueryString(this.query)).subscribe((response: any) => {
      this.queryResult = response;
      console.log(this.queryResult.items);
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
    this.query.page = page;
    this.loadData();
  }

}
