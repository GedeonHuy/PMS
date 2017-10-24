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
  public groups: any[];
  public group: any;
  public isClicked: boolean;
  isAdmin : boolean;
  isLecturer : boolean;

  projects : any[];
  lecturers : any[];
  quarters : any[];
  majors : any[];
  
  
  constructor(private _authenService : AuthenService, private _dataService: DataService, private _notificationService: NotificationService) {
    this.isClicked = false;
    this.isAdmin = false;
    this.isLecturer = false;
  }

  ngOnInit() {
    this.loadData();
    this.permissionAccess();

    this._dataService.get("/api/quarters/getall").subscribe((response : any) => {
      this.quarters = response;
    });

    this._dataService.get("/api/majors/getall").subscribe((response : any) => {
      this.majors = response;
    });

    this._dataService.get("/api/projects/getall").subscribe((response : any) => {
      this.projects = response;
    });

    this._dataService.get("/api/lecturers/getall").subscribe((response : any) => {
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
      console.log(response);
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
    this.loadgroup(id);
    this.modalAddEdit.show();
  }

  //Get Group with Id
  loadgroup(id: any) {
    this._dataService.get('/api/groups/getgroup/' + id)
      .subscribe((response: any) => {
        this.group = response;
        console.log(this.group);
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

  deletegroup(id: any) {
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
    if(user.role === "Admin" || user.role === "Lecturer") {
      this.isAdmin = true;
      this.isLecturer = true;
      console.log(this.isAdmin);
    }
  }
}
