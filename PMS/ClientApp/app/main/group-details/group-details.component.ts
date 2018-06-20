import { Observable } from "rxjs/Observable";
import "rxjs/add/observable/forkJoin";
import { AuthenService } from "./../../core/services/authen.service";

import { Component, OnInit, group, ViewChild } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { DataService } from "./../../core/services/data.service";
import { ModalDirective } from "ngx-bootstrap/modal";
import { ElementRef } from "@angular/core";
import { NotificationService } from "../../core/services/notification.service";
import { NgForm, NgModel } from "@angular/forms";
// import { Promise } from 'q';
import { async } from "@angular/core/testing";
import { validateConfig } from "@angular/router/src/config";
import { SystemConstants } from "../../core/common/system.constants";
import { forEach } from "@angular/router/src/utils/collection";

@Component({
  selector: "app-group-details",
  templateUrl: "./group-details.component.html",
  styleUrls: ["./group-details.component.scss"]
})

export class GroupDetailsComponent implements OnInit {
  @ViewChild("modalUpload") public modalUpload: ModalDirective;
  @ViewChild("modalDownload") public modalDownload: ModalDirective;
  @ViewChild("fileInput") fileInput: ElementRef;
  @ViewChild("modalMark") public modalMark: ModalDirective;
  @ViewChild("modalBoard") public modalBoard: ModalDirective;

  thisRecommendation: any;
  recommendationDescription: any;
  recommendations: any[];
  thisLecturerEmail: any;
  groupId: any;
  group: any;
  file: any;
  dataCommit: any[] = [];
  commitDetails: any[] = [];
  public uploadedFile: any;
  isHaveGithub: boolean;
  linkGithub: string;
  linkDowload: string;
  linkUploadedFileDowload: string;
  github: any;
  commits: number = 0;
  isLoadData: boolean;
  isLoadGit: boolean;
  isLoadDataCommit: boolean;
  isLoadMark: boolean;
  isLoadRecommendation: boolean;
  isOneHundred: boolean;
  isLoadGrade: boolean;
  isSendForm: boolean;
  
  temp: any[];
  groupRecommendations: any[];
  lecturers: any[];
  boardEnrollmentsOfLecturer: any[];
  groupBoardEnrollments: any[];
  thisLecturerId: any;
  thisLecturer: any;
  lecturerIds: any[];
  
  isConflict: boolean;
  hasRecommendations: boolean;
  hasBoard: boolean;
  isAdmin: boolean;
  isLecturer: boolean;
  isStudent: boolean;
  isReviewer: boolean;

  PAGE_SIZE = 5;

  query: any = {
    pageSize: this.PAGE_SIZE,
    isConfirm: "Accepted"
  };

  public todoList: Array<any>;
  public scorePercents: number[] = [
    10,
    15,
    20,
    25,
    30,
    35,
    40,
    45,
    50,
    55,
    60,
    65,
    70,
    75,
    80,
    85,
    90,
    95,
    100
  ];
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

  constructor(
    private _authenService: AuthenService,
    private route: ActivatedRoute,
    private _notificationService: NotificationService,
    private router: Router,
    private _dataService: DataService
  ) {
    this.hasBoard = false;
    this.isReviewer = false;
    this.isAdmin = false;
    this.isLecturer = false;
    this.isLoadRecommendation = false;
    this.isLoadMark = true;
    this.isSaved = false;

    this.isLoadData = false;
    this.isLoadGit = false;
    this.isLoadDataCommit = false;

    route.params.subscribe(p => {
      this.groupId = +p["id"];
      if (isNaN(this.groupId) || this.groupId <= 0) {
        router.navigate(["/main"]);
        return;
      }
    });
  }

  ngOnInit() {
    this.permissionAccess();

    this.thisRecommendation = {
      boardEnrollmentId: null,
      description: ""
    };
    Observable.forkJoin([
      this._dataService.get("/api/lecturers/getall/")
    ]).subscribe(data => {
      this.lecturers = data[0].items;
      this.lecturers = data[0].items;
    });

    this.loadGroupDetails(this.groupId);
  }

  data = {
    labels: [
      "Week 1",
      "Week 2",
      "Week 3",
      "Week 4",
      "Week 5",
      "Week 6",
      "Week 7",
      "Week 8",
      "Week 9",
      "Week 10",
      "Week 11",
      "Week 12"
    ],
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
    responsive: true
  };

  //Get Group with Id
  loadGroupDetails(id: any) {
    Observable.forkJoin([
      this._dataService.get("/api/groups/getgroup/" + id),
      this._dataService.get(
        "/api/boardenrollments/getboardenrollmentsbygroupid/" + id
      ),
      this._dataService.get(
        "/api/recommendations/getrecommendationsbygroupid/" + id
      ),
      this._dataService.get("/api/lecturers/getall/")
    ]).subscribe(data => {
      //Group-START
      this.group = data[0];
      //Check link github-START
      if (this.group.linkGitHub != null) {
        this.linkGithub = data[0].linkGitHub.replace("https://github.com/", "");
        this.linkDowload = data[0].linkGitHub + "/archive/master.zip";
        this.loadGithub(this.linkGithub);
        this.loadDataCommits(this.linkGithub + "/stats/participation");
        this.loadCommitComment(this.linkGithub + "/commits");
        this.isHaveGithub = true;
      }
      //Check link github-END
      this.isLoadData = true;
      //Group-END

      //Check if has board-START
      if (this.group.board != null) this.hasBoard = true;
      //Check if has board-END

      //Check isReviewer-START
      if (this.group.boardId != null && this.isLecturer == true) {
        if (this.thisLecturerEmail != undefined)
          this.thisLecturerId = data[3].items.find(
            l => l.email == this.thisLecturerEmail
          ).lecturerId;

        if (
          this.group.board.lecturerInformations.reviewer.lecturerId ==
          this.thisLecturerId
        ) {
          this.isReviewer = true;
        }
      }

      //Check isReviewer-END

      //flag up the form sending process
      this.isSendForm = true;
      ////////////

      //Score-START
      this.isLoadGrade = true;
      this.groupBoardEnrollments = data[1].items;
      if (this.groupBoardEnrollments.length != 0) {
        if (this.group.board.resultScore == null) this.group.resultScore = "";
        else this.group.resultScore = this.group.board.resultScore;
        if (this.group.board.resultGrade == null) this.group.resultGrade = "";
        else this.group.resultGrade = this.group.board.resultGrade;
      } else {
        this.groupBoardEnrollments = undefined;
        this.group.resultScore = "";
        this.group.resultGrade = "";
      }
      //Score-END

      //Recommendations-START
      this.isLoadRecommendation = true;
      this.groupRecommendations = data[2].items;
      //Recommendations-END
    });
  }

  loadData() {
    this._dataService
      .get("/api/groups/getall" + "?" + this.toQueryString(this.query))
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

  //Get repository
  loadGithub(link: string) {
    this._dataService.getGithub(link).subscribe((response: any) => {
      this.github = response;
      this.isLoadGit = true;
    });
  }

  loadCommitComment(link: string) {
    this._dataService.getGithub(link).subscribe((response: any) => {
      for (var i = 0; i < 3; i++) {
        this.commitDetails.push(response[i]);
      }
    });
  }

  loadDataCommits(link: string) {
    this._dataService.getGithub(link).subscribe((response: any) => {
      if (response.all.length != undefined) {
        for (var i = 40; i < response.all.length; i++) {
          this.dataCommit.push(response.all[i]);
        }

        for (var i = 0; i < response.all.length; i++) {
          this.commits = this.commits + response.all[i];
        }
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
    this.linkUploadedFileDowload =
      SystemConstants.BASE_URL + "/api/uploadfiles/downloadfile/" + id;
    this._dataService
      .get("/api/uploadfiles/getuploadedfile/" + id)
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
      this._dataService
        .upload(
          "/api/uploadfiles/addfile/" + uploadedFileId,
          nativeElement.files[0]
        )
        .subscribe(
          (response: any) => {
            this.modalUpload.hide();
            form.resetForm();
            this._notificationService.printSuccessMessage("Add File Success");
          },
          error => this._dataService.handleError(error)
        );
    }
  }

  saveBoard(form: NgForm) {
    if (form.valid) {

      //start transform string values into int numbers
      this.board.lecturerInformations.chair.scorePercent
        = parseInt(this.board.lecturerInformations.chair.scorePercent);
      this.board.lecturerInformations.secretary.scorePercent
        = parseInt(this.board.lecturerInformations.secretary.scorePercent);
      this.board.lecturerInformations.supervisor.scorePercent
        = parseInt(this.board.lecturerInformations.supervisor.scorePercent);
      this.board.lecturerInformations.reviewer.scorePercent
        = parseInt(this.board.lecturerInformations.reviewer.scorePercent);
      //end transform string values into int numbers

      //start check if sum all score percents equals 100
      this.lecturerIds = [
        this.board.lecturerInformations.chair.lecturerId,
        this.board.lecturerInformations.secretary.lecturerId,
        this.board.lecturerInformations.supervisor.lecturerId,
        this.board.lecturerInformations.reviewer.lecturerId
      ];
      this.isOneHundred =
        this.board.lecturerInformations.chair.scorePercent +
        this.board.lecturerInformations.secretary.scorePercent +
        this.board.lecturerInformations.supervisor.scorePercent +
        this.board.lecturerInformations.reviewer.scorePercent == 100;
      //end check if sum all score percents equals 100

      if (this.isOneHundred == true) {
        this.isSaved = true;
        if (this.board.boardId == undefined) {
          this._dataService
            .post("/api/boards/add", JSON.stringify(this.board))
            .subscribe(
              (response: any) => {
                this.modalBoard.hide();
                this.loadData();
                this._notificationService.printSuccessMessage("Add Success");
                form.resetForm();

                this.isSaved = false;
                this.isLoadData = false;
              },
              error => {
                this._dataService.handleError(error);
              }
            );
        } else {
          this._dataService
            .put(
              "/api/boards/update/" + this.board.boardId,
              JSON.stringify(this.board)
            )
            .subscribe(
              (response: any) => {
                this.loadData();
                this.modalBoard.hide();
                this._notificationService.printSuccessMessage("Update Success");
                form.resetForm();

                this.isSaved = false;
                this.isLoadData = false;
              },
              error => {
                form.resetForm();
                this._dataService.handleError(error);
                this.isSaved = false;
                this.isLoadData = false;
              }
            );
        }
      } else {
        this.assignBoard(this.group.groupId);

        this._notificationService.printErrorMessage(
          "Total percent must be 100%!\nPlease try again."
        );
        this.isConflict = true;
        setTimeout(() => {
          this.isConflict = false;
        },
        3000);
      }
    }
  }

  saveMark(form: NgForm) {
    if (form.valid) {
      this.isSaved = true;

      if (this.boardEnrollment.score != null) {
        this._dataService
          .put(
            "/api/boardenrollments/update/" +
              this.boardEnrollment.boardEnrollmentId,
            JSON.stringify(this.boardEnrollment)
          )
          .subscribe(
            (response: any) => {
              this.loadData();
              this._notificationService.printSuccessMessage("Update Success");
              form.resetForm();
              this.isSaved = false;
              this.isLoadData = false;
              this.isLoadMark = false;
              this.modalMark.hide();
              this.ngOnInit();
            },
            error => this._dataService.handleError(error)
          );
      }
    }
  }

  //Mark method
  mark(id: any) {
    this.modalMark.show();

    Observable.forkJoin(
      this._dataService.get(
        "/api/boardenrollments/getboardenrollmentsbylectureremail/" +
          this.thisLecturerEmail
      ),
      this._dataService.get(
        "/api/recommendations/getrecommendationsbygroupid/" + this.groupId
      )
    ).subscribe(data => {
      this.boardEnrollmentsOfLecturer = data[0].items;
      this.boardEnrollment = this.boardEnrollmentsOfLecturer.find(
        be => be.boardID == id
      );
      this.isLoadMark = true;

      this.recommendations = data[1].items;
      this.recommendations = this.recommendations.filter(
        r => r.boardEnrollmentId == this.boardEnrollment.boardEnrollmentId
      );

    });
  }

  onScorePercentChange() {
    this.isConflict = false;
  }

  onLecturerChange(selected: any, seat: any) {
    this.isConflict = false;
    
    if ((selected == this.boardEnrollments.chair.lecturerId) && !(seat === 'chair')) {
      this.isConflict = true;
      this._notificationService.printErrorMessage(
        "This lecturer is already the chair!"
      );

    } else if ((selected == this.boardEnrollments.secretary.lecturerId) && !(seat === 'secretary')) {
      this.isConflict = true;
      this._notificationService.printErrorMessage(
        "This lecturer is already the secretary!"
      );

    } else if ((selected == this.boardEnrollments.reviewer.lecturerId) && !(seat === 'reviewer')) {
      this.isConflict = true;
      this._notificationService.printErrorMessage(
        "This lecturer is already the reviewer!"
      );

    } else if ((selected == this.boardEnrollments.supervisor.lecturerId) && !(seat === 'supervisor')) {
      if (seat === 'chair') this.boardEnrollments.chair.lecturerId = null;
      if (seat === 'secretary') this.boardEnrollments.secretary.lecturerId = null;
      if (seat === 'reviewer') this.boardEnrollments.reviewer.lecturerId = null;

      this._notificationService.printErrorMessage(
        "Cannot select this lecturer because this is the supervisor!"
      );
      this.isConflict = true;
    }
    
    this.temp = [
      parseInt(this.boardEnrollments.chair.lecturerId),
      parseInt(this.boardEnrollments.secretary.lecturerId),
      parseInt(this.boardEnrollments.supervisor.lecturerId),
      parseInt(this.boardEnrollments.reviewer.lecturerId)
    ];

    console.log(this.temp);

    this.boardEnrollments.chair.lecturerId        = this.temp[0];
    this.boardEnrollments.secretary.lecturerId    = this.temp[1];
    this.boardEnrollments.supervisor.lecturerId   = this.temp[2];
    this.boardEnrollments.reviewer.lecturerId     = this.temp[3];
  }

  //Create method
  assignBoard(id: any) {
    this.modalBoard.show();
    this.isConflict = false;
    
    Observable.forkJoin(
      this._dataService.get("/api/groups/getgroup/" + id)
    ).subscribe(data => {
      this.group = data[0];

      if (this.group.board == null) {
        this.boardEnrollments = {};
        this.chair = {
          scorePercent: 25
        };
        this.secretary = {
          scorePercent: 25
        };
        this.supervisor = {
          name: this.group.lecturer.name,
          lecturerId: this.group.lecturerId,
          scorePercent: 25
        };
        this.reviewer = {
          scorePercent: 25
        };

        this.group.board = {};

        //this.lecturers = this.lecturers.filter(l => l.majorId == this.group.majorId);
        this.board.groupId = this.group.groupId;
        this.board.lecturerInformations = this.boardEnrollments;
        this.board.lecturerInformations.chair = this.chair;
        this.board.lecturerInformations.secretary = this.secretary;
        this.board.lecturerInformations.supervisor = this.supervisor;
        this.board.lecturerInformations.reviewer = this.reviewer;

        this.boardEnrollments = this.board.lecturerInformations;

        this.isLoadBoard = true;
      } else {
        this.board.groupId = this.group.groupId;
        this.board.boardId = this.group.boardId;
        this.chair = {
          name: this.lecturers.find(
            l =>
              l.lecturerId ==
              this.group.board.lecturerInformations.chair.lecturerId
          ).name,
          lecturerId: this.group.board.lecturerInformations.chair.lecturerId,
          scorePercent: this.group.board.lecturerInformations.chair.scorePercent
        };
        this.secretary = {
          name: this.lecturers.find(
            l =>
              l.lecturerId ==
              this.group.board.lecturerInformations.secretary.lecturerId
          ).name,
          lecturerId: this.group.board.lecturerInformations.secretary
            .lecturerId,
          scorePercent: this.group.board.lecturerInformations.secretary
            .scorePercent
        };
        this.supervisor = {
          name: this.lecturers.find(
            l =>
              l.lecturerId ==
              this.group.board.lecturerInformations.supervisor.lecturerId
          ).name,
          lecturerId: this.group.board.lecturerInformations.supervisor
            .lecturerId,
          scorePercent: this.group.board.lecturerInformations.supervisor
            .scorePercent
        };
        this.reviewer = {
          name: this.lecturers.find(
            l =>
              l.lecturerId ==
              this.group.board.lecturerInformations.reviewer.lecturerId
          ).name,
          lecturerId: this.group.board.lecturerInformations.reviewer.lecturerId,
          scorePercent: this.group.board.lecturerInformations.reviewer
            .scorePercent
        };

        this.boardEnrollments = {
          chair: this.chair,
          secretary: this.secretary,
          supervisor: this.supervisor,
          reviewer: this.reviewer
        };
        this.board.lecturerInformations = this.boardEnrollments;
        this.board.boardId = this.group.boardId;
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
      this.isLoadData = false;
      this.isHaveGithub = false;
      this.loadGroupDetails(this.groupId);

      this.isLoadBoard = false;
    }
  }

  hidemodalMark() {
    this.modalMark.hide();
  }

  hidemodalBoard() {
    this.modalBoard.hide();
  }

  hidemodalDownload() {
    this.modalDownload.hide();
  }

  hidemodalUpload() {
    this.modalUpload.hide();
  }

  AddUploadedFile() {
    var uploadedFileId;
    return new Promise((resolve, reject) => {
      this.uploadedFile.groupId = this.groupId;
      this._dataService
        .post("/api/uploadfiles/add", JSON.stringify(this.uploadedFile))
        .subscribe(
          (response: any) => {
            uploadedFileId = response.uploadedFileId;
            // console.log(uploadedFileId + "first");
            this._notificationService.printSuccessMessage("Add Success");
            resolve(uploadedFileId);
          },
          error => reject(this._dataService.handleError(error))
        );
    });
  }

  download(modalUpload) {
    window.location.href = this.linkUploadedFileDowload;
    this._notificationService.printSuccessMessage("Download Success");
    this.modalDownload.hide();
  }

  calculateScore(id: any) {
    this.isLoadGrade = false;
    this._dataService
      .get("/api/boards/calculatescore/" + id)
      .subscribe((response: any) => {
        this.loadGroupDetails(this.groupId);
      });
  }

  sendForm(id: any) {
    this.isSendForm = false;
    this._dataService
      .get("/api/boards/sendformtolecturers/" + id)
      .subscribe((response: any) => {
        this.loadGroupDetails(this.groupId);
      });
    // setTimeout(() => {
    //   this.loadGroupDetails(this.groupId);
    // },
    // 5000);
  }

  addRecommendation(id: any, form: NgForm) {
    this.thisRecommendation = {
      boardEnrollmentId: null,
      description: ""
    };
    this.thisRecommendation.boardEnrollmentId = id;
    this.thisRecommendation.description = "" + this.recommendationDescription;

    this._dataService
      .post("/api/recommendations/add", JSON.stringify(this.thisRecommendation))
      .subscribe(
        (response: any) => {
          this._notificationService.printSuccessMessage("Add Success");
          this.thisRecommendation.recommendationId = response.recommendationId;
          this.recommendations.push(this.thisRecommendation);
        },
        error => this._dataService.handleError(error)
      );

    form.resetForm();
  }

  removeRecommendation(i: any) {
    this._dataService
      .delete(
        "/api/recommendations/delete/" +
          this.recommendations[i].recommendationId
      )
      .subscribe(
        (response: any) => {
          this._notificationService.printSuccessMessage("Delete Success");
        }
      );

    this.recommendations.splice(i, 1);
  }

  markRecommendation(i: any) {
    if (this.groupRecommendations[i].isDone == false)
      this.groupRecommendations[i].isDone = true;
    else this.groupRecommendations[i].isDone = false;

    this._dataService
      .put(
        "/api/recommendations/update/" +
          this.groupRecommendations[i].recommendationId,
        JSON.stringify(this.groupRecommendations[i])
      )
      .subscribe(
        (response: any) => {
          this._notificationService.printSuccessMessage("Check Success");
        },
        error => this._dataService.handleError(error)
      );
  }
}
