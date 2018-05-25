
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

  constructor(private route: ActivatedRoute, private _notificationService: NotificationService,
    private router: Router, private _dataService: DataService) {

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
    this.loadGroupDetails(this.groupId);
  }

  data = {
    labels: ["Week 1", "Week 2", "Week 3", "Week 4", "Week 5", "Week 6", "Week 7", "Week 8", "Week 9", "Week 10", "Week 11", "Week 12"],
    datasets: [
      {
        label: "Commits in 10 weeks",
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
        console.log(this.group);
        this.linkGithub = response.linkGitHub.replace("https://github.com/", "");
        this.linkDowload = response.linkGitHub + "/archive/master.zip";
        this.loadGithub(this.linkGithub);
        this.loadDataCommits(this.linkGithub + "/stats/participation");
        this.loadCommitComment(this.linkGithub + "/commits");
        this.isLoadData = true;
      });
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
      console.log(uploadedFileId + "second")
      var nativeElement: HTMLInputElement = this.fileInput.nativeElement;
      console.log(nativeElement.files[0])
      this._dataService.upload('/api/uploadfiles/addfile/' + uploadedFileId, nativeElement.files[0])
        .subscribe((response: any) => {
          this.modalUpload.hide();
          form.resetForm();
          this._notificationService.printSuccessMessage("Add File Success");
        }, error => this._dataService.handleError(error));
    }
  }

  AddUploadedFile() {
    var uploadedFileId;
    return new Promise((resolve, reject) => {
      this.uploadedFile.groupId = this.groupId;
      this._dataService.post('/api/uploadfiles/add', JSON.stringify(this.uploadedFile))
        .subscribe((response: any) => {
          uploadedFileId = (response.uploadedFileId);
          console.log(uploadedFileId + "first");
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
}