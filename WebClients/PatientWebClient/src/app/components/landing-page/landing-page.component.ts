import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { IPatientFeedback } from 'src/app/interfaces/patient-feedback-interface';
import { FeedbackService } from 'src/app/services/FeedbackService/feedback.service';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css'],
})
export class LandingPageComponent implements OnInit {
  allFeedbacks!: IPatientFeedback[];
  topTenFeedbacks!: IPatientFeedback[];
  sub!: Subscription;
  numOfFeedback!: number;
  constructor(private _service: FeedbackService, private router: Router) {}

  ngOnInit(): void {
    this.sub = this._service.getPublicApproved().subscribe({
      next: (feedback) => {
        this.topTenFeedbacks = feedback;
       
      },
    });
  }

  navigate(): void {
    this.router.navigate(['/registration']);
  }
}
