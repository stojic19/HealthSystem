import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { IFeedback } from 'src/app/interfaces/feedback';
import { FeedbacksManagerService } from 'src/app/services/feedbacks-manager.service';

@Component({
  selector: 'app-feedbacks-manager',
  templateUrl: './feedbacks-manager.component.html',
  styleUrls: ['./feedbacks-manager.component.css'],
  providers: [FeedbacksManagerService]
})
export class FeedbacksManagerComponent implements OnInit {

  allComments!: IFeedback[];
  waiting!: IFeedback[];
  approved: IFeedback[] = [];
  sub! : Subscription;
  constructor(private _feedbackService: FeedbacksManagerService) {

  }

  ngOnInit(): void {
    this.allComments = [];
    this.sub = this._feedbackService.getFeedback().subscribe({
      next:  com => {this.allComments = com;}
  })}

  approveComment(feedback: IFeedback) {
    for (let c of this.allComments) {
      if (c.id === feedback.id) {
        c.feedbackStatus = 2;
        this._feedbackService.approveFeedback(c);
      }
    }
  }
}
