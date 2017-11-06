import { Observable } from 'rxjs/Observable';
import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AuthenService } from './../../core/services/authen.service';
import "rxjs/add/Observable/forkJoin";


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  @ViewChild('enrollmentModal') public enrollmentModal: ModalDirective;
  public enrollment: any;

  constructor(private _authenService: AuthenService, private _dataService: DataService, private _notificationService: NotificationService) {
    this.isLoading = false;
    this.isClicked = false;
    this.isAdmin = false;
    this.isStudent = false;
    this.isLecturer = false;
  }
  public isClicked: boolean;
  public isLoading: boolean;


  isAdmin: boolean;
  isLecturer: boolean;
  isStudent: boolean;

  projects: any[];
  lecturers: any[];
  quarters: any[];

  ngOnInit() {
    Observable.forkJoin([
      this._dataService.get("/api/quarters/getall"),


      this._dataService.get("/api/projects/getall"),

      this._dataService.get("/api/lecturers/getall")
    ]).subscribe(data => {
        this.quarters = data[0].items,
        this.projects = data[1].items,
        this.lecturers = data[2].items
    });

  }

  //Create method
  createEnrollment() {
    this.enrollmentModal.show();
    var user = this._authenService.getLoggedInUser();
    this.enrollment = {};
    this.enrollment.studentEmail = user.email;
    this.isLoading = true;
    // Observable.forkJoin(
    //   this._dataService.get('/api/groups/getgroup/' + id)
    // ).subscribe(data => {
    //   this.enrollment.groupId = id;
    //   this.enrollment.studentEmail = user.email;
    //   this.enrollment.quarterId = data[0].quarterId;
    //   this.enrollment.type = data[0].project.type;
    //   
    // });
  }

  hideEnrollmentModal() {
    this.enrollmentModal.hide();
    this.isLoading = false;
  }

  saveChange(valid: boolean) {
    if (valid) {
      if (this.enrollment.id == undefined) {
        console.log(this.enrollment.id);
        this._dataService.post('/api/enrollments/add', JSON.stringify(this.enrollment))
          .subscribe((response: any) => {
            this.enrollmentModal.hide();
            this._notificationService.printSuccessMessage("Add Success");
          }, error => this._dataService.handleError(error));
      }
    }


  }
}
