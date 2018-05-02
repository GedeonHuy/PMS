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
  isExist : boolean;

  isLoadLecturer : boolean;
  isLoadProject: boolean;

  isAdmin: boolean;
  isLecturer: boolean;
  isStudent: boolean;

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

  public typeStatus: any[] = ["Accepted", "Pending", "Denied"];

  constructor(private _authenService: AuthenService, private _dataService: DataService, private _notificationService: NotificationService) {
    this.isLoadData = false;
    this.isSaved = false;
    this.isAdmin = false;
    this.isLecturer = false;
  }

  ngOnInit() {
    this.loadData();
    this.permissionAccess();
    Observable.forkJoin([
      this._dataService.get("/api/quarters/getall"),
      this._dataService.get("/api/majors/getall"),
      this._dataService.get("/api/projects/getall"),
      this._dataService.get("/api/lecturers/getall"),

    ]).subscribe(data => {
        this.quarters = data[0].items,
        this.majors = data[1].items,
        this.projects = data[2].items
        this.lecturers = data[3].items
      });

      
  }

  onMajorChange() {
    var selectedMajor = this.majors.find(m => m.majorId == this.group.majorId).majorId;
    this._dataService.get('/api/lecturers/getlecturersbymajor/' + selectedMajor)
      .subscribe((response: any) => {
        this.lecturers = response.items;
        this.isLoadLecturer = true;
    });

    this._dataService.get('/api/projects/getprojectsbymajor/' + selectedMajor)
    .subscribe((response: any) => {
      this.projects = response.items;
      this.isLoadProject = true;
    });
  }

  loadData() {
    this._dataService.get("/api/groups/getall" + "?" + this.toQueryString(this.query)).subscribe((response: any) => {
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
    this.modalAddEdit.show();
    this.isLoadGroup = true;
  }

  handler(type: string, $event: ModalDirective) {
    if (type === "onHide" || type === "onHidden") {
      this.group = [];
      this.students = [];
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
    this.isLoadLecturer = true;
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
        console.log(response);
        this.group = response;
        for(let se of response.studentEmails) {
          this.students.push(se);
        }
        this.isExist = true;
        this.isLoadGroup = true;
      });
  }

  saveChange(form: NgForm) {
    if (form.valid) {
      this.isSaved = true;
      if (this.group.groupId == undefined) {
        this.group.studentEmails = this.students;
        this._dataService.post('/api/groups/add', JSON.stringify(this.group))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            this._notificationService.printSuccessMessage("Add Success");
            this.isSaved = false;
            this.isLoadData = false;
            this.isExist = false;

          }, error => this._dataService.handleError(error));
      }
      else {
        this.groupJson = {
          groupName: this.group.groupName,
          isConfirm: this.group.isConfirm,
          projectId: this.group.projectId,
          lecturerId: this.group.lecturerId,
          majorId: this.group.majorId,
          quarterId: this.group.quarterId,
          students: this.group.students
        };
        this._dataService.put('/api/groups/update/' + this.group.groupId, JSON.stringify(this.groupJson))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            this._notificationService.printSuccessMessage("Update Success");
            this.isSaved = false;
            this.isLoadData = false;
            this.isExist = false;
          }, error => this._dataService.handleError(error));
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

  public allStudents: IMultiSelectOption[] = [];
  public students: string[] = [];

  loadStudents() {
    this._dataService.get("/api/students/getall").subscribe((response: any) => {
      for (let student of response.items) {
        this.allStudents.push({ id: student.email, name: student.email});
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
}