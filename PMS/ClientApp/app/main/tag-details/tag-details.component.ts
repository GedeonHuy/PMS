import { Component, OnInit } from "@angular/core";
import { SystemConstants } from "../../core/common/system.constants";
import { DataService } from "../../core/services/data.service";
import { NotificationService } from "../../core/services/notification.service";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: "app-tag-details",
  templateUrl: "./tag-details.component.html"
})
export class TagDetailsComponent implements OnInit {
  query: any = {
    pageSize: SystemConstants.PAGE_SIZE
  };
  tagName: any;
  public queryResult: any = {};
  public isLoadData: boolean;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private _dataService: DataService,
    private _notificationService: NotificationService
  ) {
    route.params.subscribe(p => {
      this.tagName = p["tag"];
      if (
        this.tagName == "" ||
        this.tagName == null ||
        this.tagName == undefined
      ) {
        router.navigate(["/main/home/index"]);
        return;
      }
    });
  }

  ngOnInit() {
    this._dataService
      .get("/api/projects/getall?tagName=" + this.tagName)
      .subscribe((response: any) => {
        this.queryResult = response;
        this.isLoadData = true;
      });
  }

  //Get Project have tagName
  loadTagDetails() {}
}
