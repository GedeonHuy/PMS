import { Observable } from 'rxjs/Observable';
import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AuthenService } from './../../core/services/authen.service';
import "rxjs/add/Observable/forkJoin";
import { IMultiSelectOption, IMultiSelectSettings } from 'angular-2-dropdown-multiselect';


@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit {
  @ViewChild('modalAddEdit') public modalAddEdit: ModalDirective;
  @ViewChild('enrollmentModal') public enrollmentModal: ModalDirective;

  public groups: any[];
  public queryResult: any = {};

  public group: any = {};
  public enrollment: any;

  public isClicked: boolean;
  public isLoading: boolean;

  public isLoadEnrollment: boolean;

  isAdmin: boolean;
  isLecturer: boolean;
  isStudent: boolean;

  projects: any[];
  lecturers: any[];
  quarters: any[];
  public enrollments: any[];

  majors: any[];
  PAGE_SIZE = 5;

  query: any = {
    pageSize: this.PAGE_SIZE,
    isConfirm: "Pending"
  };

  public user : any;

  public typeStatus : any [] = ["Accepted", "Pending", "Denied"];
  

  constructor(private _authenService: AuthenService, private _dataService: DataService, private _notificationService: NotificationService) {
    this.isLoading = false;
    this.isClicked = false;
    this.isAdmin = false;
    this.isStudent = false;
    this.isLecturer = false;
    this.isLoadEnrollment = false;
  }

  ngOnInit() {
    this.loadData();
    this.permissionAccess();
    this.loadEnrollments();
    Observable.forkJoin([
      this._dataService.get("/api/quarters/getall"),
      this._dataService.get("/api/majors/getall"),
      this._dataService.get("/api/projects/getall"),
      this._dataService.get("/api/lecturers/getall"),

    ]).subscribe(data => {
      this.quarters = data[0].items,      
      this.majors = data[1].items,
      this.projects = data[2].items,
      this.lecturers = data[3].items
    });

  }

  onMajorChange() {
    var selectedMajor = this.majors.find(m => m.majorId == this.group.majorId);
    this.lecturers = selectedMajor ? selectedMajor.lecturers : [];
    this.projects = selectedMajor ? selectedMajor.projects : [];
  }

  onLecturerChange() {
    var selectedLecturer = this.lecturers.find(l => l.lecturerId == this.group.lecturerId);
    this.group.majorId = selectedLecturer.majorId;    
  }


  loadData() {
    this._dataService.get("/api/groups/getall" + "?" + this.toQueryString(this.query)).subscribe((response: any) => {
      this.queryResult = response;
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
  showAddModal() {
    this.group = {};
    this.isLoading = true;
    this.modalAddEdit.show();
  }

  //Edit method
  showEditModal(id: any) {
    this.loadGroup(id);    
    this.modalAddEdit.show();
  }

  hideAddEditModal() {
    this.modalAddEdit.hide();
    this.isLoading = false;
  }


  //Get Group with Id
  loadGroup(id: any) {
    this._dataService.get('/api/groups/getgroup/' + id)
      .subscribe((response: any) => {
        this.group = response;
        this.enrollments = response.enrollments;  
        console.log(this.enrollments);     
        console.log(this.group.enrollments);
        this.isLoading = true;
      });
  }

  saveChange(valid: boolean) {
    if (valid) {
      this.isClicked = true;
      if (this.group.groupId == undefined) {
        this.group.enrollments = this.enrollments;
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
        console.log(this.group);
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
    this.user = this._authenService.getLoggedInUser();
    if (this.user.role === "Admin") {
      this.isAdmin = true;
    }

    if (this.user.role === "Lecturer") {
      this.isLecturer = true;
    }

    if (this.user.role === "Student") {
      this.isStudent = true;
    }
  }

  onPageChange(page) {
    this.query.page = page;
    this.loadData();
  }


  getEnrollment(email: string) {
    this._dataService.get("/api/enrollments/getenrollmentbyemail/" + email)
      .subscribe((response: any) => {
        this.enrollment = response;
      });
  }

  public allEnrollments: IMultiSelectOption[] = [];

  loadEnrollments() {
    this._dataService.get("/api/enrollments/getall" + "?" + "isConfirm=Accepted").subscribe((response: any) => {
      for (let enrollment of response.items) {
        this.allEnrollments.push({ id: enrollment, name: enrollment.studentEmail + "-" + enrollment.lecturer.name });
      }
    });
  }

  // Settings configuration
  mySettings: IMultiSelectSettings = {
    pullRight: true,
    enableSearch: true,
    checkedStyle: 'fontawesome',
    buttonClasses: 'btn btn-default btn-block',
    dynamicTitleMaxItems: 3,
    displayAllSelectedText: true
  };
}
