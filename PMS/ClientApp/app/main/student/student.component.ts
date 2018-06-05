import { SystemConstants } from "./../../core/common/system.constants";
import { ProgressService } from "./../../core/services/progress.service";
import { NotificationService } from "./../../core/services/notification.service";
import { DataService } from "./../../core/services/data.service";
import {
  Component,
  OnInit,
  NgZone,
  ViewChild,
  ElementRef
} from "@angular/core";
import { ModalDirective } from "ngx-bootstrap/modal";
import { NgForm } from "@angular/forms";
import { Response } from "@angular/http";

@Component({
  selector: "app-student",
  templateUrl: "./student.component.html"
})
export class StudentComponent implements OnInit {
  //declare Modal
  @ViewChild("modalAddEdit") public modalAddEdit: ModalDirective;
  @ViewChild("modalImport") public modalImport: ModalDirective;
  @ViewChild("fileInput") fileInput: ElementRef;

  public students: any[];
  public queryResult: any = {};
  public progress: any;
  public student: any;
  public isSaved: boolean = false;
  public isLoadData: boolean = false;
  public isLoadStudent: boolean = false;
  majors: any[];

  query: any = {
    pageSize: 10
  };

  constructor(
    private _dataService: DataService,
    private _progressService: ProgressService,
    private _notificationService: NotificationService,
    private _zone: NgZone
  ) {}
  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this._dataService
      .get("/api/students/getall" + "?" + this.toQueryString(this.query))
      .subscribe((response: any) => {
        this.queryResult = response;
        this.isLoadData = true;
      });
  }

  //Create method
  showAddModal() {
    this.student = {};
    this._dataService.get("/api/majors/getall").subscribe((response: any) => {
      this.majors = response.items;
      this.isLoadStudent = true;
    });
    this.modalAddEdit.show();
  }

  //Edit method
  showEditModal(id: any) {
    this.loadStudent(id);
    this.modalAddEdit.show();
  }

  //Import method
  showImportModal(id: any) {
    this.modalImport.show();
  }

  //Get Student with Id
  loadStudent(id: any) {
    this._dataService
      .get("/api/students/getstudent/" + id)
      .subscribe((response: any) => {
        this.student = response;
        this._dataService
          .get("/api/majors/getall")
          .subscribe((response: any) => {
            this.majors = response.items;
            this.isLoadStudent = true;
          });
      });
  }

  saveChange(form: NgForm) {
    if (form.valid) {
      this.isSaved = true;
      if (this.student.id == undefined) {
        this._dataService
          .post("/api/students/add", JSON.stringify(this.student))
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
          .put(
            "/api/students/update/" + this.student.id,
            JSON.stringify(this.student)
          )
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

  uploadFile() {
    var nativeElement: HTMLInputElement = this.fileInput.nativeElement;

    this._progressService.uploadProgress.subscribe(
      progress => {
        this._zone.run(() => {
          this.progress = progress;
        });
      },
      null,
      () => {
        this.progress = null;
      }
    );

    this._dataService
      .upload("/api/students/upload/", nativeElement.files[0])
      .subscribe(
        (response: any) => {
          this.loadData();
          this.modalImport.hide();
          this._notificationService.printSuccessMessage("Import Success");
          this.isSaved = false;
        },
        error => this._dataService.handleError(error)
      );
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

  onPageChange(page) {
    this.isLoadData = false;
    this.query.page = page;
    this.loadData();
  }

  deleteStudent(id: any) {
    this._notificationService.printConfirmationDialog("Delete confirm", () =>
      this.deleteConfirm(id)
    );
  }

  deleteConfirm(id: any) {
    this.isLoadData = false;
    this._dataService
      .delete("/api/students/delete/" + id)
      .subscribe((response: Response) => {
        this._notificationService.printSuccessMessage("Delete Success");
        this.loadData();
      });
  }

  handler(type: string, $event: ModalDirective) {
    if (type === "onHide" || type === "onHidden") {
      this.student = [];
      this.isLoadStudent = false;
    }
  }
}
