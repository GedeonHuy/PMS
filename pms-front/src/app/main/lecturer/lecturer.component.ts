import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';


@Component({
  selector: 'app-lecturer',
  templateUrl: './lecturer.component.html',
  styleUrls: ['./lecturer.component.css']
})
export class LecturerComponent implements OnInit {
  @ViewChild('modalAddEdit') public modalAddEdit: ModalDirective;
  public lecturers: any[];
  public lecturer: any;
  public isClicked: boolean;
  constructor(private _dataService: DataService, private _notificationService: NotificationService) {
    this.isClicked = false;
  }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this._dataService.get("/api/lecturers/getall").subscribe((response: any) => {
      console.log(response);
      this.lecturers = response;
    });
  }

  //Create method
  showAddModal() {
    this.lecturer = {};
    this.modalAddEdit.show();
  }

  //Edit method
  showEditModal(id: any) {
    this.loadLecturer(id);
    this.modalAddEdit.show();
  }

  //Get Role with Id
  loadLecturer(id: any) {
    this._dataService.get('/api/lecturers/getlecturer/' + id)
      .subscribe((response: any) => {
        this.lecturer = response;
        console.log(this.lecturer);
      });
  }

  saveChange(valid: boolean) {
    if (valid) {
      this.isClicked = true;
      if (this.lecturer.lecturerId == undefined) {
        this._dataService.post('/api/lecturers/add', JSON.stringify(this.lecturer))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            this._notificationService.printSuccessMessage("Add Success");
            this.isClicked = false;
          }, error => this._dataService.handleError(error));
      }
      else {
        this._dataService.put('/api/lecturers/update/' + this.lecturer.lecturerId, JSON.stringify(this.lecturer))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            this._notificationService.printSuccessMessage("Update Success");
            this.isClicked = false;
          }, error => this._dataService.handleError(error));
      }
    }
  }

  deletelecturer(id: any) {
    this._notificationService.printConfirmationDialog("Delete confirm", () => this.deleteConfirm(id));
  }

  deleteConfirm(id: any) {
    this._dataService.delete('/api/lecturers/delete/' + id)
      .subscribe((response: Response) => {
        this._notificationService.printSuccessMessage("Delete Success");
        this.loadData();
      });
  }
}
