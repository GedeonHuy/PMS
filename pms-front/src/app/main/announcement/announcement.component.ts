import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { NgForm } from '@angular/forms';


@Component({
  selector: 'app-announcement',
  templateUrl: './announcement.component.html',
  styleUrls: ['./announcement.component.css']
})
export class AnnouncementComponent implements OnInit {

  @ViewChild('modalAddEdit') public modalAddEdit: ModalDirective;
  public announcements: any[];
  public isClicked: boolean;

  public entity: any;

  constructor(private _dataService: DataService, private _notificationService: NotificationService) {
    this.isClicked = false;
  }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this._dataService.get("/api/announcement/getall").subscribe((response: any) => {
      this.announcements = response;
      console.log(response);
    }, error => this._dataService.handleError(error));
  }

  //Create method
  showAddModal() {
    this.entity = {};
    this.modalAddEdit.show();
  }

  saveChange(form: NgForm) {
    if (form.valid) {
      this.isClicked = true;
      if (this.entity.id == undefined) {
        this._dataService.post('/api/announcement/add', JSON.stringify(this.entity))
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

  deleteRole(id: any) {
    this._notificationService.printConfirmationDialog("Delete confirm", () => this.deleteConfirm(id));
  }

  deleteConfirm(id: any) {
    this._dataService.delete('/api/roles/delete/' + id)
      .subscribe((response: Response) => {
        this._notificationService.printSuccessMessage("Delete Success");
        this.loadData();
      });
  }
}