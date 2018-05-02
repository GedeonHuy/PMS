import { SystemConstants } from './../../core/common/system.constants';
import { ProjectTypesConstants } from './../../core/common/projectType.constants';
import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html'
})
export class ProjectComponent implements OnInit {

  @ViewChild('modalAddEdit') public modalAddEdit: ModalDirective;

  public projects: any[];
  public project: any;
  public isSaved: boolean;
  public isLoadData: boolean;
  public isLoadProject: boolean;
  majors : any[];

  public queryResult: any = {};

  query: any = {
    pageSize: SystemConstants.PAGE_SIZE
  };

  public types: any[] = [ProjectTypesConstants.A, ProjectTypesConstants.B, ProjectTypesConstants.C, ProjectTypesConstants.D];


  constructor(private _dataService: DataService, private _notificationService: NotificationService) {
    this.isSaved = false;
    this.isLoadData = false;
  }
  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this._dataService.get("/api/projects/getall" + "?" + this.toQueryString(this.query)).subscribe((response: any) => {
      this.queryResult = response;
      this.isLoadData = true;
    });
  }

  //Create method
  showAddModal() {
    this.project = {};
    this.isLoadProject = true;
    this._dataService.get("/api/majors/getall").subscribe((response: any) => {
      this.majors = response.items;
      this.isLoadProject = true;
    });
    this.modalAddEdit.show();
  }

  //Edit method
  showEditModal(id: any) {
    this.loadproject(id);
    this.modalAddEdit.show();
  }

  //Get Role with Id
  loadproject(id: any) {
    this._dataService.get('/api/projects/getproject/' + id)
      .subscribe((response: any) => {
        this.project = response;
        this._dataService.get("/api/majors/getall").subscribe((response: any) => {
          this.majors = response.items;
          this.isLoadProject = true;
        });
      });
  }

  saveChange(form: NgForm) {
    if (form.valid) {
      this.isSaved = true;
      if (this.project.projectId == undefined) {
        this._dataService.post('/api/projects/add', JSON.stringify(this.project))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            form.resetForm();
            this._notificationService.printSuccessMessage("Add Success");
            this.isSaved = false;
            this.isLoadData = false;
          }, error => this._dataService.handleError(error));
      }
      else {
        this._dataService.put('/api/projects/update/' + this.project.projectId, JSON.stringify(this.project))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            form.resetForm();
            this._notificationService.printSuccessMessage("Update Success");
            this.isSaved = false;
            this.isLoadData = false;
          }, error => this._dataService.handleError(error));
      }
    }
  }

  deleteproject(id: any) {
    this._notificationService.printConfirmationDialog("Delete confirm", () => this.deleteConfirm(id));
  }

  deleteConfirm(id: any) {
    this.isLoadData = false;
    this._dataService.delete('/api/projects/delete/' + id)
      .subscribe((response: Response) => {
        this._notificationService.printSuccessMessage("Delete Success");
        this.loadData();
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
  onPageChange(page) {
    this.isLoadData = false;
    this.query.page = page;
    this.loadData();
  }

  handler(type: string, $event: ModalDirective) {
    if (type === "onHide" || type === "onHidden") {
      this.project = [];
      this.isLoadProject = false;
    }
  }

}
