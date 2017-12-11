import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { NgForm } from '@angular/forms';
import { SystemConstants } from './../../core/common/system.constants';

@Component({
  selector: 'app-announcement',
  templateUrl: './announcement.component.html',
  styleUrls: ['./announcement.component.css']
})
export class AnnouncementComponent implements OnInit {

  @ViewChild('modalAddEdit') public modalAddEdit: ModalDirective;
  public announcements: any[];
  public isClicked: boolean;
  isLoadData: boolean;
  public announcement: any;
  public queryResult: any = {};
  query: any = {
    pageSize: SystemConstants.PAGE_SIZE
  };
  constructor(private _dataService: DataService, private _notificationService: NotificationService) {
    this.isClicked = false;
    this.isLoadData = false;
  }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this._dataService.get("/api/announcements/getall" + "?" + this.toQueryString(this.query)).subscribe((response: any) => {
      this.queryResult = response;
      this.isLoadData = true;
    });
  }

  //Create method
  showAddModal() {
    this.announcement = {};
    this.modalAddEdit.show();
  }

  saveChange(form: NgForm) {
    if (form.valid) {
      this.isClicked = true;
      if (this.announcement.announcementId == undefined) {
        this._dataService.post('/api/announcements/add', JSON.stringify(this.announcement))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            form.resetForm();
            this.isClicked = false;
            this._notificationService.printSuccessMessage("Add Success");
          }, error => this._dataService.handleError(error));
      }
    }
  }

  deleteAnnouncement(id: any) {
    this._notificationService.printConfirmationDialog("Delete confirm", () => this.deleteConfirm(id));
  }

  deleteConfirm(id: any) {
    this._dataService.delete('/api/roles/delete/' + id)
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

  onPageChange(page) {
    this.isLoadData = false;
    this.query.page = page;
    this.loadData();
  }
}