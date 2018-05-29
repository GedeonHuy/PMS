import { Component, OnInit, ViewChild } from "@angular/core";
import { ModalDirective } from "ngx-bootstrap/modal";
import { SystemConstants } from "../../core/common/system.constants";
import { DataService } from "../../core/services/data.service";
import { NotificationService } from "../../core/services/notification.service";
import { NgForm } from "@angular/forms";
import { Response } from "@angular/http";

@Component({
  selector: "app-tag",
  templateUrl: "./tag.component.html"
})
export class TagComponent implements OnInit {
  @ViewChild("modalAddEdit") public modalAddEdit: ModalDirective;

  public tag: any;
  public queryResult: any = {};
  public isLoadData: boolean = false;
  public isLoadTag: boolean = false;
  public isSaved: boolean = false;
  query: any = {
    pageSize: SystemConstants.PAGE_SIZE
  };
  constructor(
    private _dataService: DataService,
    private _notificationService: NotificationService
  ) {}

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this._dataService
      .get("/api/tags/getall" + "?" + this.toQueryString(this.query))
      .subscribe((response: any) => {
        this.queryResult = response;
        this.isLoadData = true;
      });
  }

  toQueryString(obj) {
    var parts = [];
    for (var property in obj) {
      var value = obj[property];
      if (value != null && value != undefined)
        parts.push(
          encodeURIComponent(property) + "=" + encodeURIComponent(value)
        );
    }
    return parts.join("&");
  }

  //Create method
  showAddModal() {
    this.tag = {};
    this.isLoadTag = true;
    this.modalAddEdit.show();
  }

  //Edit method
  showEditModal(id: any) {
    this.loadTag(id);
    this.modalAddEdit.show();
  }

  //Get Tag with Id
  loadTag(id: any) {
    this._dataService
      .get("/api/tags/gettag/" + id)
      .subscribe((response: any) => {
        this.tag = response;
        this.isLoadTag = true;
      });
  }

  saveChange(form: NgForm) {
    if (form.valid) {
      this.isSaved = true;
      if (this.tag.id == undefined) {
        this._dataService
          .post("/api/tags/add", JSON.stringify(this.tag))
          .subscribe(
            (response: any) => {
              this.loadData();
              this.modalAddEdit.hide();
              form.resetForm();
              this._notificationService.printSuccessMessage("Add Success");
              this.isSaved = false;
            },
            error => {
              form.resetForm();
              this._dataService.handleError(error)
              this.isSaved = false;
              this.isLoadData =false;
            });
      } else {
        this._dataService
          .put("/api/tags/update/" + this.tag.id, JSON.stringify(this.tag))
          .subscribe(
            (response: any) => {
              this.loadData();
              this.modalAddEdit.hide();
              form.resetForm();
              this._notificationService.printSuccessMessage("Update Success");
              this.isSaved = false;
            },
            error => {
              form.resetForm();
              this._dataService.handleError(error)
              this.isSaved = false;
              this.isLoadData =false;
            });
      }
    }
  }

  deleteTag(id: any) {
    this._notificationService.printConfirmationDialog("Delete confirm", () =>
      this.deleteConfirm(id)
    );
  }

  deleteConfirm(id: any) {
    this.isLoadData = false;
    this._dataService
      .delete("/api/tags/delete/" + id)
      .subscribe((response: Response) => {
        this._notificationService.printSuccessMessage("Delete Success");
        this.loadData();
      });
  }

  handler(type: string, $event: ModalDirective) {
    if (type === "onHide" || type === "onHidden") {
      this.tag = [];
      this.isLoadTag = false;
    }
  }
}
