import { Observable } from 'rxjs/Observable';
import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AuthenService } from './../../core/services/authen.service';
import "rxjs/add/Observable/forkJoin";

@Component({
  selector: 'app-confirm-group',
  templateUrl: './confirm-group.component.html',
  styleUrls: ['./confirm-group.component.css']
})
export class ConfirmGroupComponent implements OnInit {
  @ViewChild('modalAddEdit') public modalAddEdit: ModalDirective;

  public groups: any[];
  public group: any;

  public isClicked: boolean;
  public isLoading: boolean;

  isAdmin: boolean;
  isLecturer: boolean;
  isStudent: boolean;
  
  lecturers: any[];
  lecturerInformations: any[];
  
  public council : any;

  constructor(private _authenService: AuthenService, private _dataService: DataService, private _notificationService: NotificationService) {
    this.isLoading = false;
    this.isClicked = false;
    this.isAdmin = false;
    this.isStudent = false;
    this.isLecturer = false;
  }

  ngOnInit() {
    this.loadData();
    this.permissionAccess();

    Observable.forkJoin([

      this._dataService.get("/api/lecturers/getall")
      
    ]).subscribe(data => {
        this.lecturers = data[0]
    });

    this._dataService.get("/api/councils/getcouncilenrollment/1").subscribe((response: any) => {
      console.log(response.lecturerInformations[0]);
    });
  }

  loadData() {
    this._dataService.get("/api/groups/getall?isConfirm=Accepted").subscribe((response: any) => {
      this.groups = response;
    });
  }

  //Create method
  assignCouncil(id : any) {
    this.modalAddEdit.show();
    this.council = {};
    Observable.forkJoin(
      this._dataService.get('/api/groups/getgroup/' + id)
    ).subscribe(data => {
      this.group = data[0];
      this.lecturers = this.lecturers.filter(l => l.majorId == this.group.majorId);  
      this.council.groudId = this.group.groupId;
      this.isLoading = true;      
    });       
  }

  hideAddEditModal() {
    this.modalAddEdit.hide();
    this.isLoading = false;
  }

  saveChange(valid: boolean) {
    if (valid) {
      this.isClicked = true;
      if (this.group.groupId == undefined) {
        this._dataService.post('/api/groups/add', JSON.stringify(this.group))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            this._notificationService.printSuccessMessage("Add Success");
            this.isClicked = false;
            this.isLoading = false;
          }, error => this._dataService.handleError(error));
      }
      else {
        this._dataService.put('/api/groups/update/' + this.group.groupId, JSON.stringify(this.group))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            this._notificationService.printSuccessMessage("Update Success");
            this.isClicked = false;
            this.isLoading = false;
          }, error => this._dataService.handleError(error));
      }
    }
  }

  deleteGroup(id: any) {
    this._notificationService.printConfirmationDialog("Delete confirm", () => this.deleteConfirm(id));
  }

  deleteConfirm(id: any) {
    this._dataService.delete('/api/groups/delete/' + id)
      .subscribe((response: Response) => {
        this._notificationService.printSuccessMessage("Delete Success");
        this.loadData();
      });
  }

  permissionAccess() {
    var user = this._authenService.getLoggedInUser();
    if (user.role === "Admin" || user.role === "Lecturer") {
      this.isAdmin = true;
      this.isLecturer = true;
    }
    if(user.role === "Student") {
      this.isStudent = true;
    }
  }
}
