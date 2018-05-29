import { Observable } from 'rxjs/Observable';
import "rxjs/add/observable/forkJoin";
import { AuthenService } from './../../core/services/authen.service';

import { Component, OnInit, group, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from './../../core/services/data.service';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ElementRef } from '@angular/core';
import { NotificationService } from '../../core/services/notification.service';
import { NgForm } from '@angular/forms';
// import { Promise } from 'q';
import { async } from '@angular/core/testing';
import { validateConfig } from '@angular/router/src/config';
import { SystemConstants } from '../../core/common/system.constants';

@Component({
  selector: 'app-group-details',
  templateUrl: './group-details.component.html',
  styleUrls: ['./group-details.component.scss']
})
export class GroupDetailsComponent implements OnInit {
  @ViewChild('modalUpload') public modalUpload: ModalDirective;
  @ViewChild('modalDownload') public modalDownload: ModalDirective;
  @ViewChild('fileInput') fileInput: ElementRef;
  @ViewChild('modalMark') public modalMark: ModalDirective;

  thisLecturerEmail: any;
  groupId: any;
  group: any;
  file: any;
  dataCommit: any[] = [];
  commitDetails: any[] = [];
  public uploadedFile: any;
  linkGithub: string;
  linkDowload: string;
  linkUploadedFileDowload: string;
  github: any;
  commits: number = 0;
  isLoadData: boolean;
  isLoadGit: boolean;
  isLoadDataCommit: boolean;
  isLoadMark: boolean;

  boardEnrollmentsOfLecturer: any[];

  isAdmin: boolean;
  isLecturer: boolean;
  isStudent: boolean;

  PAGE_SIZE = 5;

  query: any = {
    pageSize: this.PAGE_SIZE,
    isConfirm: "Accepted"
  };

  public isLoadBoard: boolean;
  public queryResult: any = {};
  public isSaved: boolean;
  public user: any;
  public boardEnrollment: any;

  constructor(private _authenService: AuthenService, private route: ActivatedRoute, private _notificationService: NotificationService,
    private router: Router, private _dataService: DataService) {

    this.isAdmin = false;
    this.isLecturer = false;
    this.isLoadMark = true;
    this.isSaved = false;

    this.isLoadData = false;
    this.isLoadGit = false;
    this.isLoadDataCommit = false;

    route.params.subscribe(p => {
      this.groupId = +p['id'];
      if (isNaN(this.groupId) || this.groupId <= 0) {
        router.navigate(['/main']);
        return;
      }
    });
  }

  ngOnInit() {
    this.permissionAccess();
    this.loadGroupDetails(this.groupId);
  }

  data = {
    labels: ["Week 1", "Week 2", "Week 3", "Week 4", "Week 5", "Week 6", "Week 7", "Week 8", "Week 9", "Week 10", "Week 11", "Week 12"],
    datasets: [
      {
        label: "Commits in 12 weeks",
        fill: false,
        data: this.dataCommit,
        borderColor: "#66cc66",
        borderWidth: 1
      }
    ]
  };
  options = {
    responsive: true,
  };

  //Get Group with Id
  loadGroupDetails(id: any) {
    this._dataService.get('/api/groups/getgroup/' + id)
      .subscribe((response: any) => {
        this.group = response;
        this.linkGithub = response.linkGitHub.replace("https://github.com/", "");
        this.linkDowload = response.linkGitHub + "/archive/master.zip";
        this.loadGithub(this.linkGithub);
        this.loadDataCommits(this.linkGithub + "/stats/participation");
        this.loadCommitComment(this.linkGithub + "/commits");
        this.isLoadData = true;
      });
  }

  loadData() {
    this._dataService.get("/api/groups/getall" + "?" + this.toQueryString(this.query)).subscribe((response: any) => {
      this.queryResult = response;
      this.isLoadData = true;
    });
  }

  toQueryString(obj) {
    var parts = [];
    for (var property in obj) {
      var value = obj[property];
      if (value != null && value != undefined)
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }

    return parts.join('&');
  }

  //Get repository
  loadGithub(link: string) {
    this._dataService.getGithub(link)
      .subscribe((response: any) => {
        this.github = response;
        this.isLoadGit = true;
      });
  }

  loadCommitComment(link: string) {
    this._dataService.getGithub(link)
      .subscribe((response: any) => {
        for (var i = 0; i < 3; i++) {
          this.commitDetails.push(response[i]);
        }
      });
  }

  loadDataCommits(link: string) {
    this._dataService.getGithub(link)
      .subscribe((response: any) => {
        
        for (var i = 40; i < response.all.length; i++) {
          this.dataCommit.push(response.all[i]);
        }

        for (var i = 0; i < response.all.length; i++) {
          this.commits = this.commits + response.all[i];
        }

        this.isLoadDataCommit = true;
      });
  }

  //Upload method
  showUploadModal() {
    this.uploadedFile = {};
    this.modalUpload.show();
  }

  //Download method
  showDownloadModal(id: any) {
    this.linkUploadedFileDowload=SystemConstants.BASE_URL+'/api/uploadfiles/downloadfile/'+id;
    this._dataService.get('/api/uploadfiles/getuploadedfile/' + id)
      .subscribe((response: any) => {
        this.file = response;
      });
    this.modalDownload.show();
  }

  async saveChange(form: NgForm) {
    if (form.valid) {
      var uploadedFileId = await this.AddUploadedFile();
      // console.log(uploadedFileId + "second")
      var nativeElement: HTMLInputElement = this.fileInput.nativeElement;
      // console.log(nativeElement.files[0])
      this._dataService.upload('/api/uploadfiles/addfile/' + uploadedFileId, nativeElement.files[0])
        .subscribe((response: any) => {
          this.modalUpload.hide();
          form.resetForm();
          this._notificationService.printSuccessMessage("Add File Success");
        }, error => this._dataService.handleError(error));
    }
  }

  saveMark(form: NgForm) {
    if (form.valid) {
      this.isSaved = true;

      if (this.boardEnrollment.score != null)
      { 
        this._dataService.put('/api/boardenrollments/update/' + this.boardEnrollment.boardEnrollmentId, JSON.stringify(this.boardEnrollment))
          .subscribe((response: any) => {
            this.loadData();
            this._notificationService.printSuccessMessage("Update Success");
            form.resetForm();
            this.isSaved = false;
            this.isLoadData = false;
            this.isLoadMark = false;
            this.modalMark.hide();
            this.ngOnInit();
          }, error => this._dataService.handleError(error));
      }
    }
  }

  //Mark method
  mark(id: any) {
    this.modalMark.show();

    Observable.forkJoin(
      this._dataService.get('/api/boardenrollments/getboardenrollmentsbylectureremail/' + this.thisLecturerEmail)
    ).subscribe(data => {
      this.boardEnrollmentsOfLecturer = data[0].items;
      this.boardEnrollment = this.boardEnrollmentsOfLecturer.find(be => be.boardID == id);
      this.isLoadMark = true;
    });
  }

  permissionAccess() {
    this.user = this._authenService.getLoggedInUser();
    if (this.user.role === "Admin") {
      this.isAdmin = true;
    }

    if (this.user.role === "Lecturer") {
      this.isLecturer = true;
      this.thisLecturerEmail = this.user.email;
    }

    if (this.user.role === "Student") {
      this.isStudent = true;
    }
  }
  
  handler(type: string, $event: ModalDirective) {
    if (type === "onHide" || type === "onHidden") {
      this.group = [];
      this.isLoadBoard = false;
    }
  }

  hidemodalMark() {
    this.modalMark.hide();
  }

  AddUploadedFile() {
    var uploadedFileId;
    return new Promise((resolve, reject) => {
      this.uploadedFile.groupId = this.groupId;
      this._dataService.post('/api/uploadfiles/add', JSON.stringify(this.uploadedFile))
        .subscribe((response: any) => {
          uploadedFileId = (response.uploadedFileId);
          // console.log(uploadedFileId + "first");
          this._notificationService.printSuccessMessage("Add Success");
          resolve(uploadedFileId);
        }, error => reject(this._dataService.handleError(error)));
    });
  }
  download(modalUpload){
    window.location.href = this.linkUploadedFileDowload
    this._notificationService.printSuccessMessage("Download Success");
    this.modalDownload.hide();
  }

  calculateScore(id: any){
    console.log(id);
    this._dataService.get('/api/boards/calculatescore/' + id)
      .subscribe((response: any) => {
        window.location.reload();
      });
  }

  calculateGrade(id: any){
    this._dataService.get('/api/boards/calculategrade/' + id)
      .subscribe((response: any) => {
        window.location.reload();
      });
  }
}