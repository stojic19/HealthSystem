import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { IPatientFeedback } from 'src/app/interfaces/patient-feedback-interface';
import { FeedbackService } from 'src/app/services/FeedbackService/feedback.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
})
export class HomePageComponent implements OnInit {
  allFeedbacks!: IPatientFeedback[];
  topFiveFeedbacks!: IPatientFeedback[];
  sub!: Subscription;
  numOfFeedback!: number;
  constructor(private _service: FeedbackService, private router: Router) {}

  ngOnInit(): void {
    this.sub = this._service.getApproved().subscribe({
      next: (feedback) => {
        this.allFeedbacks = feedback;
        this.topFiveFeedbacks = this.allFeedbacks.slice(
          this.allFeedbacks.length / 2
        );
      },
    });
  }

  navigate(): void {
    this.router.navigate(['/registration']);
  }
}
