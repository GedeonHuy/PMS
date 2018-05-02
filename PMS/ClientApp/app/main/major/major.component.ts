
import { SystemConstants } from './../../core/common/system.constants';
import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AuthenService } from './../../core/services/authen.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-major',
  templateUrl: './major.component.html'
})
export class MajorComponent implements OnInit {

  @ViewChild('modalAddEdit') public modalAddEdit: ModalDirective;
  public majors: any[];
  public queryResult: any = {};

  public major: any;
  public isSaved: boolean;
  public isLoadMajor = false;

  isAdmin: boolean;
  isLoadData: boolean;

  PAGE_SIZE = 2;

  query: any = {
    pageSize: SystemConstants.PAGE_SIZE
  };

  constructor(private _authenService: AuthenService, private _dataService: DataService, private _notificationService: NotificationService) {
    this.isSaved = false;
    this.isAdmin = false;
    this.isLoadData = false;
  }

  ngOnInit() {
    this.loadData();
    this.permissionAccess();
  }

  loadData() {
    this._dataService.get("/api/majors/getall" + "?" + this.toQueryString(this.query)).subscribe((response: any) => {
      this.queryResult = response;
      this.isLoadData = true;
    });
  }

  //Create method
  showAddModal() {
    this.major = {};
    this.isLoadMajor = true;
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
        this.isLoadMajor = true;
      });
  }

  saveChange(form: NgForm) {
    if (form.valid) {
      this.isSaved = true;
      if (this.major.majorId == undefined) {
        this._dataService.post('/api/majors/add', JSON.stringify(this.major))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            this._notificationService.printSuccessMessage("Add Success");
            this.isSaved = false;
            this.isLoadData = false;
          }, error => this._dataService.handleError(error));
      }
      else {
        this._dataService.put('/api/majors/update/' + this.major.majorId, JSON.stringify(this.major))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            this._notificationService.printSuccessMessage("Update Success");
            this.isSaved = false;
            this.isLoadData = false;
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
    this.isLoadData = false;
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
    }
  }
  onPageChange(page) {
    this.isLoadData = false;
    this.query.page = page;
    this.loadData();
  }
  handler(type: string, $event: ModalDirective) {
    if (type === "onHide" || type === "onHidden") {
      this.major = [];
      this.isLoadMajor = false;
    }
  }
}