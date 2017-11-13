import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AuthenService } from './../../core/services/authen.service';

@Component({
  selector: 'app-lecturer',
  templateUrl: './lecturer.component.html',
  styleUrls: ['./lecturer.component.css']
})
export class LecturerComponent implements OnInit {
  @ViewChild('modalAddEdit') public modalAddEdit: ModalDirective;
  public lecturers: any[];
  public queryResult: any = {};
  
  public lecturer: any;
  public isClicked: boolean;
  isAdmin : boolean;
  majors : any[];
  PAGE_SIZE = 3;
  
    query: any = {
      pageSize: this.PAGE_SIZE
    };

  constructor(private _authenService : AuthenService, private _dataService: DataService, private _notificationService: NotificationService) {
    this.isClicked = false;
    this.isAdmin = false;
  }

  ngOnInit() {
    this.loadData();
    this.permissionAccess();

    this._dataService.get("/api/majors/getall").subscribe((response: any) => {
      this.majors = response.items;
    });
  }

  loadData() {
    this._dataService.get("/api/lecturers/getall" + "?" + this.toQueryString(this.query)).subscribe((response: any) => {
      this.queryResult = response;
      console.log(this.queryResult.items);
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

  toQueryString(obj) {
    var parts = [];
    for (var property in obj) {
      var value = obj[property];
      if (value != null && value != undefined) 
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }

    return parts.join('&');
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

  permissionAccess() {
    var user = this._authenService.getLoggedInUser();
    if(user.role === "Admin") {
      this.isAdmin = true;
      console.log(this.isAdmin);
    }
  }
  onPageChange(page) {
    this.query.page = page;
    this.loadData();
  }

}
