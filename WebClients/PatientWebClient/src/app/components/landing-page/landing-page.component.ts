import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { IBenefit } from 'src/app/interfaces/benefit';
import { IPatientFeedback } from 'src/app/interfaces/patient-feedback-interface';
import { FeedbackService } from 'src/app/services/FeedbackService/feedback.service';
import { LandingPageService } from 'src/app/services/LandingPage/landing-page.service';
import { DomSanitizer, SafeStyle } from '@angular/platform-browser';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css'],
})
export class LandingPageComponent implements OnInit {
  allFeedbacks!: IPatientFeedback[];
  topTenFeedbacks!: IPatientFeedback[];
  sub!: Subscription;
  sub1!: Subscription;
  numOfFeedback!: number;
  benefits! : IBenefit[];
  pictures! : string[];
  constructor(private _service: FeedbackService,private _benefitService : LandingPageService, private router: Router,private sanitizer: DomSanitizer) {
    this.pictures = ["../../assets/images/10130.jpg","../../assets/images/hands-on-laptop.jpg","../../assets/images/278-1-scaled.jpg",]

  }

  ngOnInit(): void {
    this.sub = this._service.getPublicApproved().subscribe({
      next: (feedback) => {
        this.topTenFeedbacks = feedback;
      },
    });
    this.sub1 = this._benefitService.getBenefits().subscribe({
      next: (b) => {
        this.benefits = b;
        for (let i = 0; i < this.benefits.length; i++){
          this.getImage(i);
        }
      },
    });
  }
  getImage(i:number){
    this._benefitService.getImage(this.imageReq(this.benefits[i].pharmacyId))
    .subscribe(image => {
      if(image != "err"){
        this.benefits[i].imageSrc = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + image);
      }},
      (error) => alert(error.error));
  }
  imageReq(id:number){
    var imageReq = {
      PharmacyId: id
    }
    return imageReq;
  }
  navigate(): void {
    this.router.navigate(['/registration']);
  }
}
