import { Component, OnInit, group } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from './../../core/services/data.service';

@Component({
  selector: 'app-group-details',
  templateUrl: './group-details.component.html',
  styleUrls: ['./group-details.component.css']
})
export class GroupDetailsComponent implements OnInit {

  groupId: any;
  group: any;
  dataCommit: any[] = [];
  commitDetails : any[] = [];
  linkGithub : string;
  linkDowload : string;
  github: any;
  commits: number = 0;
  isLoading: boolean;
  isLoadGit: boolean;
  isLoadDataCommit : boolean;

  constructor(private route: ActivatedRoute,
    private router: Router, private _dataService: DataService) {

    this.isLoading = false;
    this.isLoadGit = false;
    this.isLoadDataCommit = false;

    route.params.subscribe(p => {
      this.groupId = +p['id'];
      if (isNaN(this.groupId) || this.groupId <= 0) {
        router.navigate(['/main/home/index']);
        return;
      }
    });
  }

  ngOnInit() {
    this.loadGroupDetails(this.groupId);
  }

  data = {
    labels: ["Week 1", "Week 2", "Week 3", "Week 4", "Week 5", "Week 6", "Week 7", "Week 8", "Week 9", "Week 10"],
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
        this.linkGithub = response.linkGitHub.replace("https://github.com/", "");
        this.linkDowload = response.linkGitHub + "/archive/master.zip";
        this.loadGithub(this.linkGithub);
        this.loadDataCommits(this.linkGithub + "/stats/participation");
        this.loadCommitComment(this.linkGithub + "/commits");
        this.isLoading = true;
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

  loadCommitComment(link : string) {
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
        for (var i = 51; i > 40; i--) {
          this.dataCommit.push(response.all[i]);
          this.commits = this.commits + response.all[i];
        }
        this.isLoadDataCommit = true;
      });      
  }
}
