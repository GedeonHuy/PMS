import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AuthenService } from './../../core/services/authen.service';

@Component({
  selector: 'app-major',
  templateUrl: './major.component.html',
  styleUrls: ['./major.component.css']
})
export class MajorComponent implements OnInit {
  @ViewChild('modalAddEdit') public modalAddEdit: ModalDirective;
  public majors: any[];
  public queryResult: any = {};

  public major: any;
  public isClicked: boolean;
  isAdmin: boolean;

  PAGE_SIZE = 2;

  query: any = {
    pageSize: this.PAGE_SIZE
  };

  constructor(private _authenService: AuthenService, private _dataService: DataService, private _notificationService: NotificationService) {
    this.isClicked = false;
    this.isAdmin = false;
  }

  ngOnInit() {
    this.loadData();
    this.permissionAccess();
  }

  loadData() {
    this._dataService.get("/api/majors/getall" + "?" + this.toQueryString(this.query)).subscribe((response: any) => {
      this.queryResult = response;
      console.log(this.queryResult);
    });
  }

  //Create method
  showAddModal() {
    this.major = {};
    this.modalAddEdit.show();
  }

  //Edit method
  showEditModal(id: any) {
    this.loadmajor(id);
    this.modalAddEdit.show();
  }

  //Get Role with Id
  loadmajor(id: any) {
    this._dataService.get('/api/majors/getmajor/' + id)
      .subscribe((response: any) => {
        this.major = response;
        console.log(this.major);
      });
  }

  saveChange(valid: boolean) {
    if (valid) {
      this.isClicked = true;
      if (this.major.majorId == undefined) {
        this._dataService.post('/api/majors/add', JSON.stringify(this.major))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            this._notificationService.printSuccessMessage("Add Success");
            this.isClicked = false;
          }, error => this._dataService.handleError(error));
      }
      else {
        this._dataService.put('/api/majors/update/' + this.major.majorId, JSON.stringify(this.major))
          .subscribe((response: any) => {
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
  deletemajor(id: any) {
    this._notificationService.printConfirmationDialog("Delete confirm", () => this.deleteConfirm(id));
  }

  deleteConfirm(id: any) {
    this._dataService.delete('/api/majors/delete/' + id)
      .subscribe((response: Response) => {
        this._notificationService.printSuccessMessage("Delete Success");
        this.loadData();
      });
  }

  permissionAccess() {
    var user = this._authenService.getLoggedInUser();
    if (user.role === "Admin") {
      this.isAdmin = true;
      console.log(this.isAdmin);
    }
  }
  onPageChange(page) {
    this.query.page = page;
    this.loadData();
  }

}
