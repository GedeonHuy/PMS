import { CommonModule } from '@angular/common';
import { NgForm } from '@angular/forms';
import { SignalrService } from './../../core/services/signalr.service';
import { SystemConstants } from './../../core/common/system.constants';
import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { HubConnection } from '@aspnet/signalr-client';
import { ProgressService } from '../../core/services/progress.service';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {

  @ViewChild('modalAddEdit') public modalAddEdit: ModalDirective;
  @ViewChild('modalImport') public modalImport: ModalDirective;
  @ViewChild('fileInput')  fileInput:ElementRef;
  public id: any;

  public students: any[];
  public queryResult: any = {};
  public progress:any;
  public student: any;
  public isClicked: boolean;
  isLoadData : boolean;

  hubConnection: HubConnection;
  majors: any[];

  query: any = {
    pageSize: SystemConstants.PAGE_SIZE
  };
  constructor(private _dataService: DataService, private _progressService:ProgressService, private _notificationService: NotificationService) {
    this.isClicked = false;
    this.isLoadData = false;
  }
  ngOnInit() {
    this.loadData();
    this.hubConnection = new HubConnection(SystemConstants.BASE_URL + "/hub");

    this.hubConnection.on('LoadData', (data: any) => {
      this.loadData();
    });

    this.hubConnection.start()
      .then(() => {
        console.log('Hub connection started')
      })
      .catch(err => {
        console.log('Error while establishing connection')
      });



    this._dataService.get("/api/majors/getall").subscribe((response: any) => {
      this.majors = response.items;
    });
  }

  loadData() {
    this._dataService.get("/api/students/getall" + "?" + this.toQueryString(this.query)).
      subscribe((response: any) => {
        this.queryResult = response;
        this.isLoadData = true;        
      });
  }

  //Create method
  showAddModal() {
    this.student = {};
    this.modalAddEdit.show();
  }

  //Edit method
  showEditModal(id: any) {
    this.loadStudent(id);
    this.modalAddEdit.show();
  }

  //Import method
  showImportModal(id: any){
    this.modalImport.show();
  }

  //Get Student with Id
  loadStudent(id: any) {
    this._dataService.get('/api/students/getstudent/' + id)
      .subscribe((response: any) => {
        this.student = response;
      });
  }

  saveChange(form: NgForm) {
    if (form.valid) {
      this.isClicked = true;
      if (this.student.id == undefined) {
        this._dataService.post('/api/students/add', JSON.stringify(this.student))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            form.resetForm();
            this.hubConnection.invoke('LoadData');
            this._notificationService.printSuccessMessage("Add Success");
            this.isClicked = false;
          }, error => this._dataService.handleError(error));
      }
      else {
        this._dataService.put('/api/students/update/' + this.student.id, JSON.stringify(this.student))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            form.resetForm();            
            this._notificationService.printSuccessMessage("Update Success");
            this.isClicked = false;
          }, error => this._dataService.handleError(error));
      }
    }
  }

  deleteStudent(id: any) {
    this._notificationService.printConfirmationDialog("Delete confirm", () => this.deleteConfirm(id));
  }

  deleteConfirm(id: any) {
    this._dataService.delete('/api/students/delete/' + id)
      .subscribe((response: Response) => {
        this._notificationService.printSuccessMessage("Delete Success");
        this.loadData();
      });
  }

  onPageChange(page) {
    this.isLoadData = false;    
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

  uploadFile(){
    var nativeElement:HTMLInputElement= this.fileInput.nativeElement;

    this._progressService.uploadProgress
      .subscribe(progress => {
        console.log(progress)
        this.progress=progress;
      });

    this._dataService.upload('/api/students/upload/',nativeElement.files[0])
      .subscribe((response: any) => {
      this.loadData();
      this.modalImport.hide();    
      this._notificationService.printSuccessMessage("Import Success");
      this.isClicked = false;
      }, error => this._dataService.handleError(error));
  }
}
