import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css']
})
export class RoleComponent implements OnInit {

  @ViewChild('modalAddEdit') public modalAddEdit: ModalDirective;
  public roles: any[];
  public entity: any;

  constructor(private _dataService: DataService, private _notificationService: NotificationService) { }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this._dataService.get("/api/roles/getall").subscribe((response: any) => {
      console.log(response);
      this.roles = response;
    }, error => this._dataService.handleError(error));
  }

  //Create method
  showAddModal() {
    this.entity = {};
    this.modalAddEdit.show();
  }

  //Edit method
  showEditModal(id: any) {
    this.loadRole(id);
    this.modalAddEdit.show();
  }

  //Get Role with Id
  loadRole(id: any) {
    this._dataService.get('/api/roles/getrole/' + id)
      .subscribe((response: any) => {
        this.entity = response;
        console.log(this.entity);
      });
  }

  saveChange(valid: boolean) {
    if (valid) {
      if (this.entity.id == undefined) {
        console.log(this.entity.id);
        this._dataService.post('/api/roles/add', JSON.stringify(this.entity))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            this._notificationService.printSuccessMessage("Add Success");
          }, error => this._dataService.handleError(error));
      }
      else {
        this._dataService.put('/api/roles/update/' + this.entity.id, JSON.stringify(this.entity))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            this._notificationService.printSuccessMessage("Update Success");
          }, error => this._dataService.handleError(error));
      }
    }
  }

  deleteRole(id : any) {
    this._notificationService.printConfirmationDialog("Delete confirm", () => this.deleteConfirm(id));
  }

  deleteConfirm(id : any) {
    this._dataService.delete('/api/roles/delete/' + id)
      .subscribe((response : Response) => {
        this._notificationService.printSuccessMessage("Delete Success");
        this.loadData();
      });
  }
}