import { UtilityService } from './../../../core/services/utility.service';
import { UrlConstants } from './../../../core/common/url.constants';
import {Component} from '@angular/core';
import { SystemConstants } from './../../../core/common/system.constants';
import { AuthenService } from './../../../core/services/authen.service';
import { LoggedInUser } from './../../../core/models/loggedin.user';
import {GlobalState} from '../../../global.state';

@Component({
  selector: 'ba-page-top',
  templateUrl: './baPageTop.html',
  styleUrls: ['./baPageTop.scss']
})
export class BaPageTop {

  public isScrolled:boolean = false;
  public isMenuCollapsed:boolean = false;
  public user: LoggedInUser;
  constructor(private _state:GlobalState, private utilityService: UtilityService,
    private authenService: AuthenService) {
    this._state.subscribe('menu.isCollapsed', (isCollapsed) => {
      this.isMenuCollapsed = isCollapsed;
    });
  }

  public toggleMenu() {
    this.isMenuCollapsed = !this.isMenuCollapsed;
    this._state.notifyDataChanged('menu.isCollapsed', this.isMenuCollapsed);
    return false;
  }

  public scrolledChanged(isScrolled) {
    this.isScrolled = isScrolled;
  }

  ngOnInit() {
    this.user = JSON.parse(localStorage.getItem(SystemConstants.CURRENT_USER));
  }
  logout() {
    localStorage.removeItem(SystemConstants.CURRENT_USER);
    this.utilityService.navigate(UrlConstants.LOGIN);
  }
}
