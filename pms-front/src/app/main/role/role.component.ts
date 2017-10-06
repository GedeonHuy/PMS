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

  @ViewChild('modalAddEdit') public modalAddEdit : ModalDirective;
  public roles : any[];
  public entity : any; 

  constructor(private _dataService : DataService, private _notificationService : NotificationService) { }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this._dataService.get("/api/roles/getall").subscribe((response : any) => { 
      console.log(response);
      this.roles = response; 
    });
  }

  showAddModal() {
    this.entity = {};
    this.modalAddEdit.show();
  }

  saveChange(valid : boolean) {
    if(valid) {
      if(this.entity.Id = undefined) {
        this._dataService.post("/api/roles/add", JSON.parse(this.entity))
        .subscribe((response : any) => {
          this.loadData();
          this.modalAddEdit.hide();
          this._notificationService.printSuccessMessage("Success");
        }, error => this._dataService.handleError(error))
      }
      else {

      }
    }
  }
}
