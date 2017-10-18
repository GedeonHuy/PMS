import { Component, OnInit } from '@angular/core';
import { LoggedInUser } from './../../core/models/loggedin.user';
import { AuthenService } from './../../core/services/authen.service';

@Component({
  selector: 'app-sidebar-menu',
  templateUrl: './sidebar-menu.component.html',
  styleUrls: ['./sidebar-menu.component.css']
})
export class SidebarMenuComponent implements OnInit {
  public user : LoggedInUser;
  isStudent : boolean;
  isLecturer: boolean;
  isAdmin: boolean;
  constructor(private _authService : AuthenService) {
    this.isStudent = false;
    this.isAdmin = false;
    this.isLecturer = false;
  }

  ngOnInit() {
    this.user = this._authService.getLoggedInUser();    
    this.checkAdmin();
    this.checkLecturer();
    this.checkStudent();
  }


  checkStudent() {
    if(this.user.role === "Student") {
      this.isStudent = true;
    }
  }

  checkLecturer() {
    if(this.user.role === "Lecturer") {
      this.isLecturer = true;
    }
  }

  checkAdmin() {
    if(this.user.role === "Admin") {
      this.isAdmin = true;
    }
  }
}
