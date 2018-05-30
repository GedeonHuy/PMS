import { Observable } from 'rxjs/Observable';
import "rxjs/add/observable/forkJoin";

import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AuthenService } from './../../core/services/authen.service';
import { IMultiSelectOption, IMultiSelectSettings } from 'angular-2-dropdown-multiselect';
import { NgForm } from '@angular/forms';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html'
})

export class GroupComponent implements OnInit {
  @ViewChild('modalAddEdit') public modalAddEdit: ModalDirective;

  public groups: any[];
  public queryResult: any = {};

  public group: any = {};
  public groupJson: any = {};
  public student: any;

  public isSaved: boolean;
  public isLoadGroup: boolean;
  public isLoadData: boolean;
  public isLoadStudent: boolean;
  isExist: boolean;
  isLoadRole : boolean;
  isLoadLecturer: boolean;
  isLoadProject: boolean;

  isAdmin: boolean;
  isLecturer: boolean;
  isStudent: boolean;

  isLoading : boolean;

  projects: any[];
  lecturers: any[];
  quarters: any[];

  majors: any[];
  PAGE_SIZE = 5;

  query: any = {
    pageSize: this.PAGE_SIZE,
    isConfirm: "Pending"
  };


  public user: any;
  public allStudents: IMultiSelectOption[] = [];
  public students: any[] = [];
  public typeStatus: any[] = ["Accepted", "Pending", "Denied"];

  constructor(private _authenService: AuthenService, private _dataService: DataService, private _notificationService: NotificationService) {
    this.isLoadData = false;
    this.isSaved = false;
    this.isAdmin = false;
    this.isLecturer = false;
  }

  ngOnInit() {
    this.permissionAccess();

    Observable.forkJoin([
      this._dataService.get("/api/quarters/getall"),
      this._dataService.get("/api/majors/getall"),
      this._dataService.get("/api/lecturers/getall"),

    ]).subscribe(data => {
      this.quarters = data[0].items,
        this.majors = data[1].items,
      this.lecturers = data[2].items
      this.isLoading = true;
    });
  }

  loadData() {
    this._dataService.get("/api/groups/getall" + "?" + this.toQueryString(this.query)).subscribe((response: any) => {
      this.queryResult = response;
      this.isLoadData = true;
    });
  }

  loadDataDependOnLecturer() {
    this._dataService.get("/api/groups/getall?pageSize=3&isConfirm=Pending&email=" + this.user.email).subscribe((response: any) => {
      this.queryResult = response;
      this.isLoadData = true;
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
    this.isExist = true;
    this.group = {};
    this.loadStudents();
    this._dataService.get('/api/lecturers/getlecturerbyemail/' + this.user.email)
      .subscribe((response: any) => {
        this.group.lecturerId = response.lecturerId;
        this.group.lecturerEmail = this.user.email;
        this.group.majorId = response.majorId;
        this.projects = response.projectDetails;

        this.isLoadGroup = true;
      });
    this.modalAddEdit.show();

  }

  handler(type: string, $event: ModalDirective) {
    if (type === "onHide" || type === "onHidden") {
      this.group = [];
      this.projects = [];
      this.students = [];
      this.isSaved = false;
      this.isExist = false;
      this.isLoadStudent = false;
      this.isLoadGroup = false;
      this.isLoadLecturer = false;
      this.isLoadProject = false;
    }
  }

  //Edit method
  showEditModal(id: any) {
    this.loadGroup(id);
    this.loadStudents();
    this._dataService.get('/api/lecturers/getlecturerbyemail/' + this.user.email)
    .subscribe((response: any) => {
      this.projects = response.projectDetails;
      this.isLoadGroup = true;
    });

    this.isLoadProject = true;
    this.modalAddEdit.show();
  }

  hideAddEditModal() {
    this.modalAddEdit.hide();
  }


  //Get Group with Id
  loadGroup(id: any) {
    this._dataService.get('/api/groups/getgroup/' + id)
      .subscribe((response: any) => {
        this.group = response;
        for (let student of this.group.studentInformations) {
          var info = student.name + " - " + student.email;
          this.students.push(info);
        }
        
        this.isExist = true;
        this.isLoadGroup = true;
      });
  }

  saveChange(form: NgForm) {
    if (form.valid) {
      this.isSaved = true;
      if (this.group.groupId == undefined) {

        this.group.studentEmails = [];

        for (let s of this.students) {
          this.group.studentEmails.push(s.split(' - ')[1]);
        }
        this._dataService.post('/api/groups/add', JSON.stringify(this.group))
          .subscribe((response: any) => {
            this.permissionAccess();
            this.modalAddEdit.hide();
            this._notificationService.printSuccessMessage("Add Success");
            form.resetForm();

            this.isSaved = false;
            this.isLoadData = false;
            this.isExist = false;

          }, error => {
            form.resetForm();
            this._dataService.handleError(error)
            this.isSaved = false;
            this.isLoadData = false;
            this.isExist = false;
          });
      }
      else {

        this.group.studentEmails = [];

        for (let s of this.students) {
          this.group.studentEmails.push(s.split(' - ')[1]);
        }

        this.groupJson = {
          groupName: this.group.groupName,
          isConfirm: this.group.isConfirm,
          projectId: this.group.projectId,
          lecturerEmail: this.user.email,
          lecturerId: this.group.lecturerId,
          majorId: this.group.majorId,
          quarterId: this.group.quarterId,
          studentEmails: this.group.studentEmails
        };
        this._dataService.put('/api/groups/update/' + this.group.groupId, JSON.stringify(this.groupJson))
          .subscribe((response: any) => {
            this.permissionAccess();
            this.modalAddEdit.hide();
            this._notificationService.printSuccessMessage("Update Success");
            form.resetForm();

            this.isSaved = false;
            this.isLoadData = false;
            this.isExist = false;
          }, error => {
            form.resetForm();
            this._dataService.handleError(error)
            this.isSaved = false;
            this.isLoadData = false;
            this.isExist = false;
          });
      }
    }
  }

  deleteGroup(id: any) {
    this._notificationService.printConfirmationDialog("Delete confirm", () => this.deleteConfirm(id));
  }

  deleteConfirm(id: any) {
    this.isLoadData = false;
    this._dataService.delete('/api/groups/delete/' + id)
      .subscribe((response: Response) => {
        this._notificationService.printSuccessMessage("Delete Success");
        this.permissionAccess();
      });
  }

  permissionAccess() {
    this.user = this._authenService.getLoggedInUser();
    if (this.user.role === "Admin") {
      this.loadData();

      this.isAdmin = true;
    }

    if (this.user.role === "Lecturer") {
      this.loadDataDependOnLecturer();
      this.isLecturer = true;
    }

    if (this.user.role === "Student") {
      this.isStudent = true;
    }
  }

  onPageChange(page) {
    this.query.page = page;
    this.permissionAccess();
  }


  loadStudents() {
    this._dataService.get("/api/students/getall").subscribe((response: any) => {
      this.allStudents = [];
      for (let student of response.items) {

        this.allStudents.push({ id: student.name + " - " + student.email, name: student.email });
      }
      this.isLoadStudent = true;
    });
  }

  // Settings configuration
  mySettings: IMultiSelectSettings = {
    //pullRight: true,
    enableSearch: true,
    checkedStyle: 'fontawesome',
    buttonClasses: 'btn btn-default btn-block',
    dynamicTitleMaxItems: 1,
    selectAddedValues: true
    //displayAllSelectedText: true
  };


  acceptGroup(id: any) {
    this._dataService.get('/api/groups/getgroup/' + id)
    .subscribe((response: any) => {
      this.group = response;     

      this.groupJson = {
        groupName: this.group.groupName,
        isConfirm: "Accepted",
        projectId: this.group.projectId,
        lecturerEmail: this.user.email,
        lecturerId: this.group.lecturerId,
        majorId: this.group.majorId,
        quarterId: this.group.quarterId,
        studentEmails: this.group.studentEmails
      };
      this._dataService.put('/api/groups/update/' + this.group.groupId, JSON.stringify(this.groupJson))
        .subscribe((response: any) => {
          this.permissionAccess();
          this._notificationService.printSuccessMessage("Group Accepted");
          this.isLoadData = false;
      });
    });
  }

  rejectGroup(id: any) {
    this._dataService.get('/api/groups/getgroup/' + id)
    .subscribe((response: any) => {
      this.group = response;     

      this.groupJson = {
        groupName: this.group.groupName,
        isConfirm: "Denied",
        projectId: this.group.projectId,
        lecturerEmail: this.user.email,
        lecturerId: this.group.lecturerId,
        majorId: this.group.majorId,
        quarterId: this.group.quarterId,
        studentEmails: this.group.studentEmails
      };
      this._dataService.put('/api/groups/update/' + this.group.groupId, JSON.stringify(this.groupJson))
        .subscribe((response: any) => {
          this.permissionAccess();
          this._notificationService.printSuccessMessage("Group Accepted");
          this.isLoadData = false;
      });
    });
  }
}