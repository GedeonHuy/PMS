import { HubConnection } from '@aspnet/signalr-client';
import { UrlConstants } from './../core/common/url.constants';
import { SystemConstants } from './../core/common/system.constants';
import { AuthenService } from './../core/services/authen.service';
import { LoggedInUser } from './../core/models/loggedin.user';
import { UtilityService } from './../core/services/utility.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  public user : LoggedInUser;
  constructor(private utilityService : UtilityService,
              private authenService : AuthenService) { }

  ngOnInit() {
    this.user = JSON.parse(localStorage.getItem(SystemConstants.CURRENT_USER));
    console.log(this.user.access_token);

    let connection =  new HubConnection(SystemConstants.BASE_URL + "/hub");
    
        connection.on("send", data => {
          console.log(data);
        });
    
        connection.start()
          .then(() => console.log("Connected"));
        
  }
  logout() {
    localStorage.removeItem(SystemConstants.CURRENT_USER);
    this.utilityService.navigate(UrlConstants.LOGIN);
  }
}
