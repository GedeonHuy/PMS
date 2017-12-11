import { SystemConstants } from './../../core/common/system.constants';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit } from '@angular/core';
import { LoggedInUser } from './../../core/models/loggedin.user';
import { AuthenService } from './../../core/services/authen.service';

@Component({
  selector: 'app-top-menu',
  templateUrl: './top-menu.component.html',
  styleUrls: ['./top-menu.component.css']
})
export class TopMenuComponent implements OnInit {
  public user: LoggedInUser;
  constructor(private _authService: AuthenService, private _dataService : DataService) { }

  ngOnInit() {
    this.user = this._authService.getLoggedInUser();
    this.loadAnnouncements();
  }

  public queryResult: any = {};
  query: any = {
    pageSize: SystemConstants.PAGE_SIZE
  };
  loadAnnouncements() {
    this._dataService.get("/api/announcements/getall" + "?" + this.toQueryString(this.query)).subscribe((response: any) => {
      this.queryResult = response;
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
}
