import { SystemConstants } from './core/common/system.constants';
import { Component, AfterViewChecked, ElementRef } from '@angular/core';
import { HubConnection } from '@aspnet/signalr-client';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewChecked {
  constructor(private elementRef: ElementRef) {

  }

  ngOnInit() {
    let connection =  new HubConnection(SystemConstants.BASE_URL + "/hub");

    connection.on("send", data => {
      console.log(data);
    });

    connection.start().then(() => console.log("Connected"));
  }

  ngAfterViewChecked() {
    var s = document.createElement("script");
    s.type = "text/javascript";
    s.src = "../assets/js/custom.js";
    this.elementRef.nativeElement.appendChild(s);
  }
}