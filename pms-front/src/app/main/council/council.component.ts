import { Observable } from 'rxjs/Observable';
import { NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SignalrService } from './../../core/services/signalr.service';
import { SystemConstants } from './../../core/common/system.constants';
import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { HubConnection } from '@aspnet/signalr-client';
import { LoggedInUser } from './../../core/models/loggedin.user';
import { AuthenService } from './../../core/services/authen.service';

@Component({
  selector: 'app-council',
  templateUrl: './council.component.html',
  styleUrls: ['./council.component.css']
})
export class CouncilComponent implements OnInit {

  public user: LoggedInUser;
  isLecturer: boolean;
  isAdmin: boolean;

  @ViewChild('modalAddEdit') public modalAddEdit: ModalDirective;
  @ViewChild('modalCouncilEdit') public councilAddEdit: ModalDirective;

  public id: any;

  public temp = '';

  public councils: any[];
  public queryResult: any = {};
  public council: any;
  public updateCouncil: any;
  public group: any;
  public councilEnrollments = {};
  public updateResult: any = {};

  public isLoading: boolean;
  public isLoadData: boolean;
  public isClicked: boolean;
  public isLoadGroup: boolean;
  public isShowGroup: boolean;
  public scorePercents: any[] = [25, 50, 75, 100];

  groups: any[];
  lecturers: any[];
  percentage: any[];

  public lecturerInformations: any = {};
  public president: any = {};
  public supervisor: any = {};
  public secretary: any = {};
  public reviewer: any = {};

  PAGE_SIZE = 10;

  query: any = {
    pageSize: this.PAGE_SIZE
  };

  constructor(private _dataService: DataService, private _authService: AuthenService, private _notificationService: NotificationService) {
    this.isLoading = false;
    this.isClicked = false;
    this.isLoadData = false;
    this.isLoadGroup = false;
    this.isShowGroup = false;
    this.isAdmin = false;
    this.isLecturer = false;
  }

  ngOnInit() {

    this.user = this._authService.getLoggedInUser();
    this.checkAdmin();
    this.checkLecturer();

    Observable.forkJoin([

      this._dataService.get("/api/lecturers/getall"),
      this._dataService.get("/api/groups/getall")

    ]).subscribe(data => {
      this.lecturers = data[0].items;
      this.groups = data[1].items;
      this.isLoading = true;
    });

    this.loadData();
  }

  assignScore(id: any) {
    this._dataService.get("/api/councils/calculatescore/" + id).subscribe((response: any) => {
      this._notificationService.printSuccessMessage("Calculation Success");
      this.isClicked = false;
    });
  }

  assignGrade(id: any) {
    this._dataService.get("/api/councils/calculategrade/" + id).subscribe((response: any) => {
      this._notificationService.printSuccessMessage("Assign Success");
      this.isClicked = false;
    });
  }

  loadData() {
    this._dataService.get("/api/councils/getall").subscribe((response: any) => {
      this.councils = response.items;
      this.isLoadData = true;
    });
  }

  //Create method
  showAddModal() {
    this.council = {};
    this.modalAddEdit.show();

  }

  onSelectGroup() {
    this.councilEnrollments = {};
    this.president = {};
    this.secretary = {};
    this.supervisor = {};
    this.isLoadGroup = true;
    
    Observable.forkJoin(
      this._dataService.get('/api/groups/getgroup/' + this.council.groupId)
    ).subscribe(data => {
      this.group = data[0];
      this.lecturers = this.lecturers.filter(l => l.majorId == this.group.majorId);
      this.council.groupId = this.group.groupId;
      this.council.lecturerInformations = this.councilEnrollments;
      this.council.lecturerInformations.president = this.president;
      this.council.lecturerInformations.secretary = this.secretary;
      this.council.lecturerInformations.supervisor = this.supervisor;
      this.isLoadGroup = false;
      
      this.isShowGroup = true;
    });
  }

  //Edit method
  showEditModal(id: any) {
    this.loadCouncil(id);
    this.councilAddEdit.show();
  }

  //Get council with Id
  loadCouncil(id: any) {
    this._dataService.get('/api/councils/getcouncil/' + id)
      .subscribe((response: any) => {
        this.council = response;
        console.log(this.council);
      });
  }

  // loadCouncilEnrollment(id: any) {
  //   this._dataService.get('/api/councilenrollments/getcouncilenrollmentsbycouncilid/' + id)
  //     .subscribe((response: any) => {
  //       this.councilEnrollments = response.items;
  //       this.isLoading = true;
  //       console.log(this.councilEnrollments);
  //     });
  //
  //}

  saveChange(valid: boolean) {
    if (valid) {
      this.isClicked = true;
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
        this.lecturerInformations = {};
        this.lecturerInformations = this.council.lecturerInformations;  
        
        this.updateCouncil = {
            "groupId": this.council.groupId,
            "lecturerInformations": {
              "president": {
                "lecturerId" : this.lecturerInformations.president.lecturerId,
                "scorePercent": this.lecturerInformations.president.scorePercent
              },
              "reviewer": {
                "lecturerId" : this.lecturerInformations.reviewer.lecturerId,
                "scorePercent": this.lecturerInformations.reviewer.scorePercent
              },
              "secretary": {
                "lecturerId" : this.lecturerInformations.secretary.lecturerId,
                "scorePercent": this.lecturerInformations.secretary.scorePercent
              },
              "supervisor": {
                "lecturerId" : this.lecturerInformations.supervisor.lecturerId,
                "scorePercent": this.lecturerInformations.supervisor.scorePercent
              }
            },
        };
        console.log(this.updateCouncil)
        this._dataService.put('/api/councils/update/' + this.council.councilId, JSON.stringify(this.updateCouncil))
          .subscribe((response: any) => {
            this.loadData();
            this.councilAddEdit.hide();
            this._notificationService.printSuccessMessage("Update Success");
            this.isClicked = false;
          }, error => this._dataService.handleError(error));
       }
    }
  }

  deletecouncil(id: any) {
    this._notificationService.printConfirmationDialog("Delete confirm", () => this.deleteConfirm(id));
  }

  deleteConfirm(id: any) {
    this._dataService.delete('/api/councils/delete/' + id)
      .subscribe((response: Response) => {
        this._notificationService.printSuccessMessage("Delete Success");
        this.loadData();
      });
  }

  onPageChange(page) {
    this.query.page = page;
    this.loadData();
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

  checkLecturer() {
    if (this.user.role === "Lecturer") {
      this.isLecturer = true;
    }
  }

  checkAdmin() {
    if (this.user.role === "Admin") {
      this.isAdmin = true;
    }
  }
}
