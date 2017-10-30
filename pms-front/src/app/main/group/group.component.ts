import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AuthenService } from './../../core/services/authen.service';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit {
  @ViewChild('modalAddEdit') public modalAddEdit: ModalDirective;
  @ViewChild('enrollmentModal') public enrollmentModal: ModalDirective;

  public groups: any[];
  public group: any;
  public isClicked: boolean;

  public enrollment: any;

  isAdmin: boolean;
  isLecturer: boolean;

  projects: any[];
  lecturers: any[];
  quarters: any[];
  majors: any[];
  groupToJoin : any;
  
  constructor(private _authenService: AuthenService, private _dataService: DataService, private _notificationService: NotificationService) {
    this.isClicked = false;
    this.isAdmin = false;
    this.isLecturer = false;
  }

  ngOnInit() {
    this.loadData();
    this.permissionAccess();

    this._dataService.get("/api/enrollments/getall").subscribe((response: any) => {
      console.log(response);
    });

    this._dataService.get("/api/quarters/getall").subscribe((response: any) => {
      this.quarters = response;
    });

    this._dataService.get("/api/majors/getall").subscribe((response: any) => {
      this.majors = response;
    });

    this._dataService.get("/api/projects/getall").subscribe((response: any) => {
      this.projects = response;
    });

    this._dataService.get("/api/lecturers/getall").subscribe((response: any) => {
      this.lecturers = response;
    });
  }

  onMajorChange() {
    var selectedMajorLecturer = this.lecturers.filter(l => l.majorId == this.group.majorId);
    this.lecturers = selectedMajorLecturer;
    var selectedMajorProject = this.projects.filter(p => p.majorId == this.group.majorId);
    this.projects = selectedMajorProject;
  }


  loadData() {
    this._dataService.get("/api/groups/getall").subscribe((response: any) => {
      this.groups = response;
    });
  }

  //Create method
  showAddModal() {
    this.group = {};
    this.modalAddEdit.show();
  }

  //Edit method
  showEditModal(id: any) {
    this.loadGroup(id);
    this.modalAddEdit.show();
  }

  //Get Group with Id
  loadGroup(id: any) {
    this._dataService.get('/api/groups/getgroup/' + id)
      .subscribe((response: any) => {
        this.group = response;
      });
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
          }, error => this._dataService.handleError(error));
      }
      else {
        this._dataService.put('/api/groups/update/' + this.group.groupId, JSON.stringify(this.group))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            this._notificationService.printSuccessMessage("Update Success");
            this.isClicked = false;
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
      console.log(this.isAdmin);
    }
  }

  joinGroup(id: any) {
    var user = this._authenService.getLoggedInUser();
    this._dataService.get('/api/groups/getgroup/' + id)
      .subscribe((response: any) => {
        this.groupToJoin = response;
      });
    this.enrollment = {};
    console.log(this.groupToJoin);
    this.enrollment.groupId = id;
    this.enrollment.studentEmail = user.email;
    this.enrollment.quarterId = this.groupToJoin.quarterId;
    this.enrollmentModal.show();
  }

  applyEnrollment(valid: boolean) {
    if (valid) {
      this.isClicked = true;
      if (this.enrollment.enrollmentId == undefined) {
        this._dataService.post('/api/enrollments/add', JSON.stringify(this.enrollment))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            this._notificationService.printSuccessMessage("Add Success");
            this.isClicked = false;
          }, error => this._dataService.handleError(error));
      }
      else {
        this._dataService.put('/api/enrollments/update/' + this.enrollment.enrollmentId, JSON.stringify(this.enrollment))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            this._notificationService.printSuccessMessage("Update Success");
            this.isClicked = false;
          }, error => this._dataService.handleError(error));
      }
    }
  }

}
