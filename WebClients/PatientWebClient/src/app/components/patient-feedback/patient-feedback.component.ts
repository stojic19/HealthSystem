import { IPatientFeedback, FeedbackStatus } from './../../interfaces/patient-feedback/patient-feedback-interface'
import { FeedbackService } from './../../services/FeedbackService/feedback.service';
import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-patient-feedback',
  templateUrl: './patient-feedback.component.html',
  styleUrls: ['./patient-feedback.component.css']
})
export class PatientFeedbackComponent implements OnInit {
  allFeedbacks!: IPatientFeedback[];
  approvedFeedbacks!: IPatientFeedback[];
  topTenFeedbacks!: IPatientFeedback[];
  approvedStatus: FeedbackStatus = FeedbackStatus.Approved;
  sub!: Subscription;
  numOfFeedback!: number;

  constructor(private _service: FeedbackService) { }

  ngOnInit(): void {

    this.sub = this._service.getApproved().subscribe({
      next: feedback => {
        this.allFeedbacks = feedback;
      }
    });
  }
}
