import { SystemConstants } from './../../core/common/system.constants';
import { ProgressService } from './../../core/services/progress.service';
import { NotificationService } from 'app/core/services/notification.service';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit, NgZone, ViewChild, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html'
})
export class StudentComponent implements OnInit {
  //declare Modal
  @ViewChild('modalAddEdit') public modalAddEdit: ModalDirective;

  public students: any[];
  public queryResult: any = {};
  public progress: any;
  public student: any;
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
