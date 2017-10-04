import { DataService } from './../../core/services/data.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css']
})
export class RoleComponent implements OnInit {

  public roles : any[];

  constructor(private _dataService : DataService) { }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this._dataService.get("/api/roles/getall").subscribe((response : any) => { 
      console.log(response);
      this.roles = response; 
    });
  }
}
