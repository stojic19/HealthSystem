import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { IFeedback } from 'src/app/interfaces/feedback';
import { FeedbacksManagerService } from 'src/app/services/feedbacks-manager.service';
import { MatSnackBar } from '@angular/material/snack-bar';

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
  constructor(private _feedbackService: FeedbacksManagerService, private _snackBar: MatSnackBar) {

  }

  ngOnInit(): void {
    this.allComments = [];
    this.sub = this._feedbackService.getFeedback().subscribe({
      next:  com => {this.allComments = com;}
  })}

  approveComment(feedback: IFeedback) {
    feedback.feedbackStatus = 2;
    this._feedbackService.approveFeedback(feedback);
    this._snackBar.open("Your feedback has been successfully submitted.", "Dismiss");
  }
}
