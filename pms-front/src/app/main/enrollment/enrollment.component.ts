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
  public user: any;
  public enrollment: any;
  public isClicked: boolean;
  public isLoading: boolean;

  isAdmin: boolean;
  isLecturer: boolean;
  isLoadData: boolean;
  PAGE_SIZE = 3;

  queryAdmin: any = {
    pageSize: this.PAGE_SIZE,
    isConfirm: "Pending"
  };

  query: any = {
    pageSize: this.PAGE_SIZE,
    isConfirm: "Pending"
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
    this.permissionAccess();
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
            this.loadDataAdmin();
            this.modalAddEdit.hide();
            this._notificationService.printSuccessMessage("Add Success");
            this.isClicked = false;
          }, error => this._dataService.handleError(error));
      }
      else {
        this._dataService.put('/api/enrollments/update/' + this.enrollment.enrollmentId, JSON.stringify(this.enrollment))
          .subscribe((response: any) => {
            this.loadDataAdmin();
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
        this.loadDataAdmin();
      });
  }

  permissionAccess() {
    if (this.user.role === "Admin") {
      this.isAdmin = true;
    }

    if (this.user.role === "Lecturer") {
      this.isLecturer = true;
    }
  }

  onPageChange(page) {
    this.isLoadData = false;
    this.queryAdmin.page = page;
    if (this.user.role === "Admin") {
      this.loadDataAdmin();
    }

    if (this.user.role === "Lecturer") {
      this.loadData();
    }
  }

}
