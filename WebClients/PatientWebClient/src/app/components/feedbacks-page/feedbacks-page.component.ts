import { Component, Input, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { IPatientFeedback } from 'src/app/interfaces/patient-feedback-interface';
import { FeedbackService } from 'src/app/services/FeedbackService/feedback.service';

@Component({
  selector: 'app-feedbacks-page',
  templateUrl: './feedbacks-page.component.html',
  styleUrls: ['./feedbacks-page.component.css']
})
export class FeedbacksPageComponent implements OnInit {
  allFeedbacks!: IPatientFeedback[];

  sub!: Subscription;
  constructor(private _service: FeedbackService) { }

  ngOnInit(): void {
    this.sub = this._service.getApproved().subscribe({
      next: feedback => {
        this.allFeedbacks = feedback;
      }
    });
  }

}
