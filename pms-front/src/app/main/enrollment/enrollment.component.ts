import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AuthenService } from './../../core/services/authen.service';

@Component({
  selector: 'app-enrollment',
  templateUrl: './enrollment.component.html',
  styleUrls: ['./enrollment.component.css']
})
export class EnrollmentComponent implements OnInit {
  @ViewChild('modalAddEdit') public modalAddEdit: ModalDirective;
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
    isConfirm : "Pending"
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
    this.permissionAccess();
  }

  loadData() {
    this._dataService.get("/api/enrollments/getall" + "?" + this.toQueryString(this.query)).subscribe((response: any) => {
      this.queryResult = response;
      console.log("/api/enrollments/getall" + "?" + this.toQueryString(this.query));
    });
  }

  //Create method
  showAddModal() {
    this.enrollment = {};
    this.modalAddEdit.show();
  }

  //Edit method
  showEditModal(id: any) {
    this.loadEnrollment(id);
    this.modalAddEdit.show();
  }

  //Get enrollment with Id
  loadEnrollment(id: any) {
    this._dataService.get('/api/enrollments/getenrollment/' + id)
      .subscribe((response: any) => {
        this.enrollment = response;
      });
  }

  saveChange(valid: boolean) {
    if (valid) {
      this.isClicked = true;
      if (this.enrollment.enrollmentId == undefined) {
        this._dataService.post('/api/enrollments/add', JSON.stringify(this.enrollment))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            this._notificationService.printSuccessMessage("Add Success");
            this.isClicked = false;
          }, error => this._dataService.handleError(error));
      }
      else {
        console.log(this.enrollment);
        this._dataService.put('/api/enrollments/update/' + this.enrollment.enrollmentId, JSON.stringify(this.enrollment))
          .subscribe((response: any) => {
            console.log(this.enrollment);
            this.loadData();
            this.modalAddEdit.hide();
            this._notificationService.printSuccessMessage("Update Success");
            this.isClicked = false;
          }, error => this._dataService.handleError(error));
      }
    }
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
  deletEnrollment(id: any) {
    this._notificationService.printConfirmationDialog("Delete confirm", () => this.deleteConfirm(id));
  }

  deleteConfirm(id: any) {
    this._dataService.delete('/api/enrollments/delete/' + id)
      .subscribe((response: Response) => {
        this._notificationService.printSuccessMessage("Delete Success");
        this.loadData();
      });
  }

  permissionAccess() {
    var user = this._authenService.getLoggedInUser();
    if (user.role === "Admin") {
      this.isAdmin = true;
    }

    if (user.role === "enrollment") {
      this.isLecturer = true;
    }
  }
  onPageChange(page) {
    this.query.page = page;
    this.loadData();
  }

}
