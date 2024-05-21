import { HttpClient } from '@angular/common/http';
import { Component, OnInit, inject } from '@angular/core';
import { VotingServiceService } from './service/voting-service.service';
import { ICandidateDeatils, IVoterDeatils } from './service/modals';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'VotingAppWeb';
  voterDetails?: IVoterDeatils[];
  candidateDeatils?: ICandidateDeatils[];
  selectedCandidateId?: number = -1;
  selectedVoterId?: number = -1;
  user?: string;
  addUserName?: string | null;
  toaster = inject(ToastrService);

  constructor(public readonly votingServiceService: VotingServiceService, public httpClient: HttpClient, public toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.GetVoters();
    this.GetCandidates();
  }

  GetVoters(): void {
    this.votingServiceService.GetData<IVoterDeatils[]>('api', 'Voter').subscribe({
      next: (response) => {
        this.voterDetails = response;
      }
    });
  }

  GetCandidates(): void {
    this.votingServiceService.GetData<ICandidateDeatils[]>('api', 'Candidate').subscribe({
      next: (response) => {
        this.candidateDeatils = response;
      }
    });
  }

  castVote() {
    debugger
    if (!this.selectedCandidateId || !this.selectedVoterId) {
      alert('Please select both a candidate and a voter.');
      //this.toastr.warning('Please select both a candidate and a voter.', 'Warning');
      return;
    }

    this.httpClient.post(`https://localhost:7231/api/Candidate/vote?candidateId=${Number(this.selectedCandidateId)}&voterId=${Number(this.selectedVoterId)}`, {})
      .subscribe(response => {
        this.toastr.success('Voted successfully!', 'Success');
        this.ngOnInit();
        this.selectedVoterId = -1;
        this.selectedCandidateId = -1;
      }, error => {
        if (error.status === 400 && error.error === "Voter already voted!!") {
          this.toastr.error('Voter already voted!!', 'Error');
        } else {
          this.toastr.error('Please select both a candidate and a voter.', 'Error');
        }
      });
  }

  addUser() {
    console.log(this.addUserName)
    if (!this.addUserName) {
      this.toastr.warning('Enter ' + this.user + ' name!', 'Warning');
      return;
    }
    else {
      this.httpClient.post(`https://localhost:7231/api/${this.user}`, { name: this.addUserName })
        .subscribe(response => {
          this.ngOnInit();
          this.toastr.success(this.user + ' added successfully!', 'Success');
          this.addUserName = null;
        }, error => {
          this.toastr.error('Something went wrong.', 'Error');
        });
    }
  }
}
