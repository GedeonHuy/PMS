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
  @ViewChild('modalBoard') public modalBoard: ModalDirective;

  thisLecturerEmail: any;
  groupId: any;
  group: any;
  file: any;
  dataCommit: any[] = [];
  commitDetails: any[] = [];
  public uploadedFile: any;
  isHaveGithub : boolean;
  linkGithub: string;
  linkDowload: string;
  linkUploadedFileDowload: string;
  github: any;
  commits: number = 0;
  isLoadData: boolean;
  isLoadGit: boolean;
  isLoadDataCommit: boolean;
  isLoadMark: boolean;

  lecturers: any[];
  boardEnrollmentsOfLecturer: any[];
  thisLecturerId: any;  

  isAdmin: boolean;
  isLecturer: boolean;
  isStudent: boolean;
  isReviewer: boolean;

  PAGE_SIZE = 5;

  query: any = {
    pageSize: this.PAGE_SIZE,
    isConfirm: "Accepted"
  };

  public scorePercents: number[] = [10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100];
  public board: any = {};
  public chair: any;
  public secretary: any;
  public supervisor: any;
  public reviewer: any;
  public boardEnrollments: any;
  public isLoadBoard: boolean;
  public queryResult: any = {};
  public isSaved: boolean;
  public user: any;
  public boardEnrollment: any;

  constructor(private _authenService: AuthenService, private route: ActivatedRoute, private _notificationService: NotificationService,
    private router: Router, private _dataService: DataService) {

    this.isReviewer = false;  
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

    Observable.forkJoin([
      this._dataService.get("/api/lecturers/getall/")
    ]).subscribe(data => {
      this.lecturers = data[0].items
    });
  }

  data = {
    labels: ["Week 1", "Week 2", "Week 3", "Week 4", "Week 5", "Week 6", "Week 7", "Week 8", "Week 9", "Week 10", "Week 11", "Week 12"],
    datasets: [
      {
        label: "Commits in 12 weeks",
        fill: false,
        data: this.dataCommit,
        borderColor: "black",
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
        //if (response.linkGithub != null) {
          this.linkGithub = response.linkGitHub.replace("https://github.com/", "");
          this.linkDowload = response.linkGitHub + "/archive/master.zip";
          this.loadGithub(this.linkGithub);
          this.loadDataCommits(this.linkGithub + "/stats/participation");
          this.loadCommitComment(this.linkGithub + "/commits");

          this.isHaveGithub = true;
        //}
        this.isLoadData = true;
        if (this.group.board.resultScore == null)
        {
          this.group.resultScore = "N\\A";
        }
        else
        {
          this.group.resultScore = this.group.board.resultScore;
        }
        if (this.group.board.resultGrade == null)
        {
          this.group.resultGrade = "N\\A";
        }
        else
        {
          this.group.resultGrade = this.group.board.resultGrade;
        }
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

  saveBoard(form: NgForm) {
    if (form.valid) {
      this.isSaved = true;

      if (this.board.boardId == undefined) {
        this._dataService.post('/api/boards/add', JSON.stringify(this.board))
          .subscribe((response: any) => {
            this.modalBoard.hide();
            this.loadData();
            this._notificationService.printSuccessMessage("Add Success");
            form.resetForm();

            this.isSaved = false;
            this.isLoadData =false;
          }, error => {
            this._dataService.handleError(error);
            if (this.boardEnrollments.chair.scorePercent + this.boardEnrollments.secretary.scorePercent + this.boardEnrollments.supervisor.scorePercent + this.boardEnrollments.reviewer.scorePercent != 100)
            {
              this._notificationService.printErrorMessage("Total percent must be 100%!");
            }
          });
      }
      else {
        this._dataService.put('/api/boards/update/' + this.board.boardId, JSON.stringify(this.board))
          .subscribe((response: any) => {
            this.loadData();
            this.modalBoard.hide();
            this._notificationService.printSuccessMessage("Update Success");
            form.resetForm();

            this.isSaved = false;
            this.isLoadData =false;
          }, error => {
            form.resetForm();
            this._dataService.handleError(error)
            this.isSaved = false;
            this.isLoadData =false;
          });
      }
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
      this._dataService.get('/api/boardenrollments/getboardenrollmentsbylectureremail/' + this.thisLecturerEmail),
      this._dataService.get('/api/boards/getboard/' + id),
    ).subscribe(data => {
      this.boardEnrollmentsOfLecturer = data[0].items;
      this.boardEnrollment = this.boardEnrollmentsOfLecturer.find(be => be.boardID == id);
      this.isLoadMark = true;

      this.thisLecturerId = this.lecturers.find(l => l.email == this.thisLecturerEmail).lecturerId;
      console.log(this.lecturers.find(l => l.email == this.thisLecturerEmail));
      if(data[1].lecturerInformations.reviewer.lecturerId == this.thisLecturerId)
      {
        this.isReviewer = true;
      }
    });
  }

  //Create method
  assignBoard(id: any) {
    this.modalBoard.show();
    this.boardEnrollments = {};
    this.chair = {};
    this.secretary = {};
    this.supervisor = {};
    this.reviewer = {};

    Observable.forkJoin(
      this._dataService.get('/api/groups/getgroup/' + id)
    ).subscribe(data => {
      this.group = data[0];

      if (this.group.board == null) {
        this.group.board = {};

        //this.lecturers = this.lecturers.filter(l => l.majorId == this.group.majorId);
        this.board.groupId = this.group.groupId;
        this.board.lecturerInformations = this.boardEnrollments;
        this.board.lecturerInformations.chair = this.chair;
        this.board.lecturerInformations.secretary = this.secretary;
        this.board.lecturerInformations.supervisor = this.supervisor;
        this.board.lecturerInformations.reviewer = this.reviewer;

        this.isLoadBoard = true;
      } else {

        this.board.groupId = this.group.groupId;
        this.chair = {
          name: this.lecturers.find(l => l.lecturerId == this.group.board.lecturerInformations.chair.lecturerId).name,
          lecturerId: this.group.board.lecturerInformations.chair.lecturerId,
          scorePercent: this.group.board.lecturerInformations.chair.scorePercent
        };
        this.secretary = {
          name: this.lecturers.find(l => l.lecturerId == this.group.board.lecturerInformations.secretary.lecturerId).name,
          lecturerId: this.group.board.lecturerInformations.secretary.lecturerId,
          scorePercent: this.group.board.lecturerInformations.secretary.scorePercent
        };
        this.supervisor = {
          name: this.lecturers.find(l => l.lecturerId == this.group.board.lecturerInformations.supervisor.lecturerId).name,
          lecturerId: this.group.board.lecturerInformations.supervisor.lecturerId,
          scorePercent: this.group.board.lecturerInformations.supervisor.scorePercent
        };
        this.reviewer = {
          name: this.lecturers.find(l => l.lecturerId == this.group.board.lecturerInformations.reviewer.lecturerId).name,
          lecturerId: this.group.board.lecturerInformations.reviewer.lecturerId,
          scorePercent: this.group.board.lecturerInformations.reviewer.scorePercent
        };
        
        this.boardEnrollments = {
          chair: this.chair,
          secretary: this.secretary,
          supervisor: this.supervisor,
          reviewer: this.reviewer
        };
        this.board.lecturerInformations = this.boardEnrollments;

        this.isLoadBoard = true;
      }
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
    this.loadGroupDetails(this.groupId);
    this.modalMark.hide();
  }

  hidemodalBoard() {
    this.loadGroupDetails(this.groupId);
    this.modalBoard.hide();
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
    // console.log(id);
    this._dataService.get('/api/boards/calculatescore/' + id)
      .subscribe((response: any) => {
      });
    window.location.reload()
  }
}