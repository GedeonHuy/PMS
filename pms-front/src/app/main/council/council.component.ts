import { Observable } from 'rxjs/Observable';
import { NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SignalrService } from './../../core/services/signalr.service';
import { SystemConstants } from './../../core/common/system.constants';
import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { HubConnection } from '@aspnet/signalr-client';

@Component({
  selector: 'app-council',
  templateUrl: './council.component.html',
  styleUrls: ['./council.component.css']
})
export class CouncilComponent implements OnInit {

  @ViewChild('modalAddEdit') public modalAddEdit: ModalDirective;

  public id : any;
  
  public temp = '';

  public councils: any[];  
  public queryResult: any = {};
  public council: any;

  public councilEnrollments: any[];

  public isLoading: boolean;
  public isClicked: boolean;
  hubConnection: HubConnection;
  groups: any[];
  
  PAGE_SIZE = 10;

  query: any = {
    pageSize: this.PAGE_SIZE
  };
  constructor(private _dataService: DataService, private _notificationService: NotificationService) {
    this.isLoading = false;
    this.isClicked = false;
  }

  ngOnInit() {

    this._dataService.get("/api/groups/getall").subscribe((response: any) => {
      this.groups = response.items;
    });
    
    this.loadData();
  }

  loadData() {
    this._dataService.get("/api/councils/getall").subscribe((response: any) => {
      this.councils = response.items;
    });
  }

  //Create method
  showAddModal() {
    this.council = {};

    this.modalAddEdit.show();
  }
  
  //Edit method
  showEditModal(id: any) {
    this.loadCouncilEnrollments(id);
    this.modalAddEdit.show();
  }

  //Get council with Id
  loadCouncilEnrollments(id: any) {
    this._dataService.get('/api/councilenrollments/getcouncilenrollmentsbycouncilid/' + id)
      .subscribe((response: any) => {
        this.councilEnrollments = response.items;

        console.log(response);
      });
  }
  saveChange(valid: boolean) {
    if (valid) {
      this.isClicked = true;
      if (this.council.id == undefined) {
        this._dataService.post('/api/councils/add', JSON.stringify(this.council))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            this.hubConnection.invoke('LoadData');
            this._notificationService.printSuccessMessage("Add Success");
            this.isClicked = false;
          }, error => this._dataService.handleError(error));
          console.log(this.council);
      }
      else {
        this._dataService.put('/api/councils/update/' + this.council.id, JSON.stringify(this.council))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
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
    this._dataService.delete('/api/councils/delete/' + id)
      .subscribe((response: Response) => {
        this._notificationService.printSuccessMessage("Delete Success");
        this.loadData();
      });
  }

  onPageChange(page) {
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
}
