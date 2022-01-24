import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { IBenefit } from 'src/app/interfaces/benefit';
import { IPatientFeedback } from 'src/app/interfaces/patient-feedback-interface';
import { FeedbackService } from 'src/app/services/FeedbackService/feedback.service';
import { LandingPageService } from 'src/app/services/LandingPage/landing-page.service';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css'],
})
export class LandingPageComponent implements OnInit {
  allFeedbacks!: IPatientFeedback[];
  topFiveFeedbacks!: IPatientFeedback[];
  sub!: Subscription;
  sub1!: Subscription;
  numOfFeedback!: number;
  benefits! : IBenefit[];
  pictures! : string[];
  constructor(private _service: FeedbackService,private benefitService : LandingPageService, private router: Router) {
    this.pictures = ["../../assets/images/10130.jpg","../../assets/images/hands-on-laptop.jpg","../../assets/images/278-1-scaled.jpg",]

  }

  ngOnInit(): void {
    this.sub = this._service.getApproved().subscribe({
      next: (feedback) => {
        this.allFeedbacks = feedback;
        this.topFiveFeedbacks = this.allFeedbacks.slice(
          this.allFeedbacks.length / 2
        );
      },
    });
    this.sub1 = this.benefitService.getBenefits().subscribe({
      next: (b) => {
        this.benefits = b;
        
      },
    });
  }

  navigate(): void {
    this.router.navigate(['/registration']);
  }
}
