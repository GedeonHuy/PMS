import { Observable } from 'rxjs/Observable';
import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AuthenService } from './../../core/services/authen.service';
import "rxjs/add/Observable/forkJoin";
import { query } from '@angular/core/src/animation/dsl';

@Component({
  selector: 'app-confirm-group',
  templateUrl: './confirm-group.component.html',
  styleUrls: ['./confirm-group.component.css']
})
export class ConfirmGroupComponent implements OnInit {
  @ViewChild('modalAddEdit') public modalAddEdit: ModalDirective;

  public queryResult: any = {};

  public group: any;

  public isClicked: boolean;
  public isLoading: boolean;

  isAdmin: boolean;
  isLecturer: boolean;
  isStudent: boolean;

  lecturers: any[];
  lecturerInformations: any[];

  public councilEnrollment: any;
  public council: any;
  public president: any;
  public secretary: any;
  public supervisor: any;
  public reviewer: any;

  public scorePercents: any[] = [25, 50, 75, 100];


  PAGE_SIZE = 10;

  query: any = {
    pageSize: this.PAGE_SIZE
  };

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
      this.lecturers = data[0].items
    });
  }

  loadData() {
    this._dataService.get("/api/groups/getall?isConfirm=Accepted").subscribe((response: any) => {
      this.queryResult = response;
      console.log(response.items);
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
  
  //Create method
  assignCouncil(id: any) {
    this.modalAddEdit.show();
    this.council = {};
    this.councilEnrollment = {};
    this.president = {};
    this.secretary = {};
    this.supervisor = {};
    this.reviewer = {};

    Observable.forkJoin(
      this._dataService.get('/api/groups/getgroup/' + id)
    ).subscribe(data => {
      this.group = data[0];
      this.lecturers = this.lecturers.filter(l => l.majorId == this.group.majorId);
      this.council.groupId = this.group.groupId;
      this.council.lecturerInformations = this.councilEnrollment;
      this.council.lecturerInformations.president = this.president;
      this.council.lecturerInformations.secretary = this.secretary;
      this.council.lecturerInformations.supervisor = this.supervisor;
      this.council.lecturerInformations.reviewer = this.reviewer;

      this.isLoading = true;
    });
  }

  //Edit method
  showEditModal(id: any) {
    this.modalAddEdit.show();
    this.loadAssignCouncil(id);
  }

  loadAssignCouncil(id: any) {
    this._dataService.get("/api/councils/getcouncilenrollment/" + id).subscribe((response: any) => {
      this.council = response;
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
      console.log(this.council);
      if (this.council.councilId == undefined) {
        this._dataService.post('/api/councils/add', JSON.stringify(this.council))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            this._notificationService.printSuccessMessage("Add Success");
            this.isClicked = false;
            this.isLoading = false;
          }, error => this._dataService.handleError(error));
      }
      else {
        this._dataService.put('/api/councils/update/' + this.council.councilId, JSON.stringify(this.council))
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
    this._dataService.delete('/api/councils/delete/' + id)
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
    if (user.role === "Student") {
      this.isStudent = true;
    }
  }
}
