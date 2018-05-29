import { SystemConstants } from './../../core/common/system.constants';
import { ProgressService } from './../../core/services/progress.service';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, NgZone, ViewChild, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { Response } from '@angular/http';
import { DatePipe } from '@angular/common';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-quarter',
  templateUrl: './quarter.component.html'
})
export class QuarterComponent implements OnInit {

  @ViewChild('modalAddEdit') public modalAddEdit: ModalDirective;
  public quarters: any[];
  public quarter: any;
  public isSaved: boolean;
  public isLoadQuarter: boolean;
  public queryResult: any = {};

  isLoadData: boolean;

  query: any = {
    pageSize: SystemConstants.PAGE_SIZE
  };

  constructor(private _dataService: DataService, private _notificationService: NotificationService) {
    this.isSaved = false;
    this.isLoadData = false;
  }

  ngOnInit() {
    this.loadData();
  }

  public dateOptions: any = {
    locale: { format: 'DD/MM/YYYY' },
    alwaysShowCalendars: false,
    singleDatePicker: true
  };

  loadData() {
    this._dataService.get("/api/quarters/getall" + "?" + this.toQueryString(this.query)).subscribe((response: any) => {
      this.queryResult = response;
      console.log(this.queryResult);  
      this.isLoadData = true;
    });
  }

  handler(type: string, $event: ModalDirective) {
    if (type === "onHide" || type === "onHidden") {
      this.quarter = [];
      this.isLoadQuarter = false;
    }
  }

  //Create method
  showAddModal() {
    this.quarter = {};
    this.isLoadQuarter = true;
    this.modalAddEdit.show();
  }

  //Edit method
  showEditModal(id: any) {
    this.loadquarter(id);
    this.modalAddEdit.show();
  }

  //Get Role with Id
  loadquarter(id: any) {
    this._dataService.get('/api/quarters/getquarter/' + id)
      .subscribe((response: any) => {
        this.quarter = response;
        this.isLoadQuarter = true;
      });
  }

  saveChange(form: NgForm) {
    if (form.valid) {
      this.isSaved = true;
      if (this.quarter.quarterId == undefined) {
        this._dataService.post('/api/quarters/add', JSON.stringify(this.quarter))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            form.resetForm();
            this._notificationService.printSuccessMessage("Add Success");
            this.isSaved = false;
            this.isLoadData = false;
          }, error => {
            form.resetForm();
            this._dataService.handleError(error)
            this.isSaved = false;
            this.isLoadData =false;
          });
      }
      else {
        this._dataService.put('/api/quarters/update/' + this.quarter.quarterId, JSON.stringify(this.quarter))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            form.resetForm();
            this._notificationService.printSuccessMessage("Update Success");
            this.isSaved = false;
            this.isLoadData = false;
          },error => {
            form.resetForm();
            this._dataService.handleError(error)
            this.isSaved = false;
            this.isLoadData =false;
          });
      }
    }
  }

  deleteQuarter(id: any) {
    this._notificationService.printConfirmationDialog("Delete confirm", () => this.deleteConfirm(id));
  }

  deleteConfirm(id: any) {
    this.isLoadData = false;
    this._dataService.delete('/api/quarters/delete/' + id)
      .subscribe((response: Response) => {
        this._notificationService.printSuccessMessage("Delete Success");
        this.loadData();
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

}
