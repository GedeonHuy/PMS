import { PAGES_MENU_STUDENT } from './main.menuStudent';
import { PAGES_MENU_LECTURER } from './main.menuLecturer';
import { PAGES_MENU_ADMIN } from './main.menuAdmin';
import { SystemConstants } from './../core/common/system.constants';
import { UtilityService } from './../core/services/utility.service';
import { LoggedInUser } from './../core/models/loggedin.user';
import { AuthenService } from './../core/services/authen.service';
import { Component, OnInit } from '@angular/core';
import { Routes } from '@angular/router';
import { BaMenuService } from '../theme';
@Component({
  selector: 'pages',
  template: `
    <ba-sidebar></ba-sidebar>
    <ba-page-top></ba-page-top>
    <div class="al-main">
      <div class="al-content">
        <ba-content-top></ba-content-top>
        <router-outlet></router-outlet>
      </div>
    </div>
    <footer class="al-footer clearfix">
      <div class="al-footer-main clearfix">
        <div class="al-copy">&copy; <a href="http://eiu.edu.vn" translate>{{'general.eiu'}}</a> 2018</div>
      </div>
    </footer>
    <ba-back-top position="200"></ba-back-top>
    `
})
export class Main implements OnInit {
  public user: LoggedInUser;

  constructor(private _menuService: BaMenuService, private utilityService: UtilityService,
    private authenService: AuthenService) {
  }

  ngOnInit() {
    this.user = JSON.parse(localStorage.getItem(SystemConstants.CURRENT_USER));
    if (this.user.role == "Admin") {
      this._menuService.updateMenuByRoutes(<Routes>PAGES_MENU_ADMIN);
    }


    if (this.user.role == "Lecturer") {
      this._menuService.updateMenuByRoutes(<Routes>PAGES_MENU_LECTURER);
    }

    if (this.user.role == "Student") {
      this._menuService.updateMenuByRoutes(<Routes>PAGES_MENU_STUDENT);
    }
  }
}
