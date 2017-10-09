import { Response } from '@angular/http';
import { NotificationService } from './../../core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {

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
    });
  }
}
