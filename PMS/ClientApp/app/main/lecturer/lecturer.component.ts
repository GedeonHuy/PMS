import { AuthenService } from './../../core/services/authen.service';
import { SystemConstants } from './../../core/common/system.constants';
import { ProjectTypesConstants } from './../../core/common/projectType.constants';
import { ProgressService } from './../../core/services/progress.service';
import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, ViewChild, ElementRef, NgZone } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-lecturer',
  templateUrl: './lecturer.component.html'
})
export class LecturerComponent implements OnInit {
  @ViewChild('modalAddEdit') public modalAddEdit: ModalDirective;
  @ViewChild('modalImport') public modalImport: ModalDirective;
  @ViewChild('fileInput') fileInput: ElementRef;

  public lecturers: any[];
  public queryResult: any = {};

  public lecturer: any;
  public isSaved: boolean;
  public isLoadLecturer: boolean;
  public isLoadData: boolean;
  public isAdmin: boolean;
  public progress: any;
  majors: any[];

  query: any = {
    pageSize: SystemConstants.PAGE_SIZE
  };

  constructor(private _authenService: AuthenService, private _dataService: DataService,
    private _notificationService: NotificationService, private _progressService: ProgressService,
    private _zone: NgZone) {
    this.isSaved = false;
    this.isAdmin = false;
    this.isLoadData = false;
  }

  ngOnInit() {
    this.loadData();
    this.permissionAccess();
  }

  loadData() {
    this._dataService.get("/api/lecturers/getall" + "?" + this.toQueryString(this.query)).subscribe((response: any) => {
      this.queryResult = response;
      this.isLoadData = true;
    });
  }

  //Create method
  showAddModal() {
    this.lecturer = {};
    this.isLoadLecturer = true;
    this._dataService.get("/api/majors/getall").subscribe((response: any) => {
      this.majors = response.items;
      this.isLoadLecturer = true;
    });
    this.modalAddEdit.show();
  }

  //Edit method
  showEditModal(id: any) {
    this.loadLecturer(id);
    this.modalAddEdit.show();
  }

  //Import method
  showImportModal(id: any) {
    this.modalImport.show();
  }

  //Get Role with Id
  loadLecturer(id: any) {
    this._dataService.get('/api/lecturers/getlecturer/' + id)
      .subscribe((response: any) => {
        this.lecturer = response;
        this._dataService.get("/api/majors/getall").subscribe((response: any) => {
          this.majors = response.items;
          this.isLoadLecturer = true;
        });
      });
  }

  saveChange(form: NgForm) {
    if (form.valid) {
      this.isSaved = true;
      if (this.lecturer.lecturerId == undefined) {
        this._dataService.post('/api/lecturers/add', JSON.stringify(this.lecturer))
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
        this._dataService.put('/api/lecturers/update/' + this.lecturer.lecturerId, JSON.stringify(this.lecturer))
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

  uploadFile() {
    var nativeElement: HTMLInputElement = this.fileInput.nativeElement;

    this._progressService.uploadProgress
      .subscribe(progress => {
        console.log(progress)
        this._zone.run(() => {
          this.progress = progress;
        });
      }, null,
        () => { this.progress = null; });

    this._dataService.upload('/api/lecturers/upload/', nativeElement.files[0])
      .subscribe((response: any) => {
        this.loadData();
        this.modalImport.hide();
        this._notificationService.printSuccessMessage("Import Success");
        this.isSaved = false;
      }, error => this._dataService.handleError(error));
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
  deletelecturer(id: any) {
    this._notificationService.printConfirmationDialog("Delete confirm", () => this.deleteConfirm(id));
  }

  deleteConfirm(id: any) {
    this.isLoadData = false;
    this._dataService.delete('/api/lecturers/delete/' + id)
      .subscribe((response: Response) => {
        this._notificationService.printSuccessMessage("Delete Success");
        this.loadData();
      });
  }

  permissionAccess() {
    var user = this._authenService.getLoggedInUser();
    if (user.role === "Admin") {
      this.isAdmin = true;
    }
  }
  onPageChange(page) {
    this.isLoadData = false;
    this.query.page = page;
    this.loadData();
  }

  handler(type: string, $event: ModalDirective) {
    if (type === "onHide" || type === "onHidden") {
      this.lecturer = [];
      this.isLoadLecturer = false;
    }
  }
}