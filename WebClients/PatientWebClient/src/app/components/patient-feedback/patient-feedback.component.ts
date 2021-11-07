
import { IPatientFeedback, FeedbackStatus } from './../../interfaces/patient-feedback-interface';
import { FeedbackService } from './../../services/FeedbackService/feedback.service';
import { Component, OnInit } from '@angular/core';


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

  constructor(private _service: FeedbackService) { }




  ngOnInit(): void {
    this._service.getAll().subscribe(feedbacks => {
      this.allFeedbacks = feedbacks;
      this.topTenFeedbacks = this.allFeedbacks.slice(0, 10);
      this.approvedFeedbacks = this.allFeedbacks.filter((feedback: IPatientFeedback) =>
        feedback.FeedbackStatus == this.approvedStatus);

    });


    console.log(this.allFeedbacks.length);
    console.log(this.approvedFeedbacks.length);
    console.log(this.topTenFeedbacks);

  }


}


