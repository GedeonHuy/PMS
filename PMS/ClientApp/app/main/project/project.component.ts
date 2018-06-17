import { AuthenService } from './../../core/services/authen.service';
import { SystemConstants } from './../../core/common/system.constants';
import { ProjectTypesConstants } from './../../core/common/projectType.constants';
import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, ViewChild, ElementRef, NgZone } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { NgForm } from '@angular/forms';
import { IMultiSelectOption, IMultiSelectSettings } from 'angular-2-dropdown-multiselect';
import { ProgressService } from '../../core/services/progress.service';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html'
})
export class ProjectComponent implements OnInit {

  @ViewChild('modalAddEdit') public modalAddEdit: ModalDirective;
  @ViewChild('modalImport') public modalImport: ModalDirective;
  @ViewChild('fileInput') fileInput: ElementRef;

  public projects: any[];
  public project: any;
  public isSaved: boolean;
  public isLoadData: boolean;
  public isLoadProject: boolean;
  public isLoadTag: boolean;
  public progress: any;
  majors: any[];
  lecturers :  any[];
  isExist: boolean;
  public tags: string[] = [];
  public allTags: IMultiSelectOption[] = [];
  public user : any;
  public queryResult: any = {};


  isAdmin: boolean;
  isLecturer: boolean;

  query: any = {
    pageSize: SystemConstants.PAGE_SIZE
  };

  public types: any[] = [ProjectTypesConstants.A, ProjectTypesConstants.B, ProjectTypesConstants.C, ProjectTypesConstants.D];


  constructor(private _authenService: AuthenService, private _dataService: DataService, private _notificationService: NotificationService,
    private _progressService: ProgressService, private _zone: NgZone) {
    this.isSaved = false;
    this.isLoadData = false;
  }
  ngOnInit() {
    this.loadData();
    this.permissionAccess();
  }


  permissionAccess() {
    this.user = this._authenService.getLoggedInUser();
    if (this.user.role === "Admin") {
      this.isAdmin = true;
    }

    if (this.user.role === "Lecturer") {
      this.isLecturer = true;
    }
  }

  loadData() {
    this._dataService.get("/api/projects/getall" + "?" + this.toQueryString(this.query)).subscribe((response: any) => {
      this.queryResult = response;
      this.isLoadData = true;
    });
  }

  //Create method
  showAddModal() {
    this.isExist = true;

    this.loadTags();
    this.project = {};
    if (this.user.role == "Lecturer") {
      this._dataService.get('/api/lecturers/getlecturerbyemail/' + this.user.email)
      .subscribe((response: any) => {
        this.project.majorId = response.majorId;
        this.project.lecturerId = response.lecturerId;
      });
    }

    if (this.user.role == "Admin") {
      this._dataService.get('/api/lecturers/getall')
      .subscribe((response: any) => {
        this.lecturers = response.items;
      });
    }

    this._dataService.get("/api/majors/getall").subscribe((response: any) => {
      this.majors = response.items;
      this.isLoadProject = true;
    });
    this.modalAddEdit.show();
  }

  //Edit method
  showEditModal(id: any) {
    this.loadproject(id);
    this.loadTags();
    this.modalAddEdit.show();
  }

  //Import method
  showImportModal(id: any) {
    this.modalImport.show();
  }

  //Get Role with Id
  loadproject(id: any) {
    this._dataService.get('/api/projects/getproject/' + id)
      .subscribe((response: any) => {
        this.project = response;
        for (let se of response.tags) {
          this.tags.push(se);
        }
        this._dataService.get("/api/majors/getall").subscribe((response: any) => {
          this.majors = response.items;
          this.isLoadProject = true;
          this.isExist = true;
        });
      });
  }

  saveChange(form: NgForm) {
    if (form.valid) {
      this.isSaved = true;

      if (this.project.projectId == undefined) {
        this.project.tags = this.tags;
        this._dataService.post('/api/projects/add', JSON.stringify(this.project))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            form.resetForm();
            this._notificationService.printSuccessMessage("Add Success");
            this.isSaved = false;
            this.isLoadData = false;
            this.isExist = false;

          }, error => {
            form.resetForm();
            this._dataService.handleError(error)
            this.isSaved = false;
            this.isLoadData =false;
          });
      }
      else {
        this.project.tags = this.tags;
        this._dataService.put('/api/projects/update/' + this.project.projectId, JSON.stringify(this.project))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            form.resetForm();
            this._notificationService.printSuccessMessage("Update Success");
            this.isSaved = false;
            this.isLoadData = false;
            this.isExist = false;

          }, error => {
            form.resetForm();
            this._dataService.handleError(error)
            this.isSaved = false;
            this.isLoadData =false;
          });
      }
    }
  }

  uploadFile() {
    var nativeElement: HTMLInputElement = this.fileInput.nativeElement;

    this._progressService.uploadProgress
      .subscribe(progress => {
        this._zone.run(() => {
          this.progress = progress;
        });
      }, null,
        () => { this.progress = null; });

    this._dataService.upload('/api/projects/upload/', nativeElement.files[0])
      .subscribe((response: any) => {
        this.loadData();
        this.modalImport.hide();
        this._notificationService.printSuccessMessage("Import Success");
        this.isSaved = false;
      }, error => this._dataService.handleError(error));
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
      this.similaryProjects = [];
      this.tags = [];
      this.allTags = [];
      this.isLoadProject = false;
      this.isExist = false;
      this.isLoadTag = false;
    }
  }

  loadTags() {
    this._dataService.get("/api/tags/getall").subscribe((response: any) => {
      this.allTags = [];
      for (let tag of response.items) {
        this.allTags.push({ id: tag.tagName, name: tag.tagName });
      }
      this.isLoadTag = true;
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

  isGetSimilaryProject : boolean;
  similaryProjects: any[];
  isCheckProject : boolean;
  analyzeProject(description: string) {
    this.isGetSimilaryProject = true;

    this._dataService.post('/api/projects/analyzeproject', JSON.stringify(description))
        .subscribe((response: any) => {
          this.similaryProjects = response;
          console.log(this.similaryProjects.length);
          this.isGetSimilaryProject = false;
          this.isCheckProject = true;
        }, error => {
          this._dataService.handleError(error)
        });
    }

}
