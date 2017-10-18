import { Component, OnInit } from '@angular/core';
import { LoggedInUser } from './../../core/models/loggedin.user';
import { AuthenService } from './../../core/services/authen.service';

@Component({
  selector: 'app-top-menu',
  templateUrl: './top-menu.component.html',
  styleUrls: ['./top-menu.component.css']
})
export class TopMenuComponent implements OnInit {
  public user : LoggedInUser;
  constructor(private _authService : AuthenService) { }

  ngOnInit() {
    this.user = this._authService.getLoggedInUser();
  }

}
