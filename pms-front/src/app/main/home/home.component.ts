import { AuthenService } from './../../core/services/authen.service';
import { Component, OnInit } from '@angular/core';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private _authenService : AuthenService) { }

  ngOnInit() {
    var user = this._authenService.getLoggedInUser();
    console.log(user);
  }

}
