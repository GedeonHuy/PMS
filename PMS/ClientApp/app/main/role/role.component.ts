import { SystemConstants } from './../../core/common/system.constants';
import { ProgressService } from './../../core/services/progress.service';
import { NotificationService } from 'app/core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, NgZone, ViewChild, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { Response } from '@angular/http';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html'
})
export class RoleComponent implements OnInit {

  //declare Modal
  @ViewChild('modalAddEdit') public modalAddEdit: ModalDirective;

  public roles: any = [];
  public role: any;
  public isClicked: boolean = false;
  public isLoadData: boolean = false;

  query: any = {
    pageSize: SystemConstants.PAGE_SIZE
  };

  constructor(private _dataService: DataService, private _progressService: ProgressService,
    private _notificationService: NotificationService, private _zone: NgZone) {
  }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this._dataService.get("/api/roles/getall").subscribe((response: any) => {
      this.roles = response;
      this.isLoadData = true;
    })
  }

  //Create method
  showAddModal() {
    this.role = {};
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
        this.role = response;
      });
  }

  saveChange(valid: boolean) {
    if (valid) {
      if (this.role.id == undefined) {
        this._dataService.post('/api/roles/add', JSON.stringify(this.role))
          .subscribe((response: any) => {
            this.loadData();
            this.modalAddEdit.hide();
            this._notificationService.printSuccessMessage("Add Success");
          }, error => this._dataService.handleError(error));
      }
      else {
        this._dataService.put('/api/roles/update/' + this.role.id, JSON.stringify(this.role))
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
