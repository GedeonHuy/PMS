import { Component, OnInit } from '@angular/core';
import { NotificationService } from '../core/services/notification.service';
import { AuthenService } from '../core/services/authen.service';

import { Router } from '@angular/router';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loading = false;
  model: any = {};
  returnUrl: string;
  constructor(private authenService: AuthenService,
    private notificationService: NotificationService,
    private router: Router) { }

  ngOnInit() {
  }
  login() {
    this.loading = true;
    this.authenService.login(this.model.email, this.model.password).subscribe(data => {
      this.router.navigate(["/main/home/index"]);
    }, error => {
      this.notificationService.printErrorMessage("error");
      this.loading = false;
    });
  }

}