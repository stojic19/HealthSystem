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

  allFeedbacks!: IFeedback[];
  waiting!: IFeedback[];
  approved: IFeedback[] = [];
  sub! : Subscription;
  constructor(private _feedbackService: FeedbacksManagerService, private _snackBar: MatSnackBar) {

  }

  ngOnInit(): void {
    this.allFeedbacks = [];
    this.sub = this._feedbackService.getFeedback().subscribe(
     com => {this.allFeedbacks = com;
    console.log(com)}
  )}

  approveFeedback(feedback: IFeedback) {
    this._feedbackService.approveFeedback(feedback).subscribe(editedFeedback => {
      let index = this.allFeedbacks.indexOf(feedback);
      this.allFeedbacks[index] = editedFeedback;
    },
      error => {                             
        console.error('Error approving feedback!' + error.HttpErrorResponse)
    });
    this._snackBar.open("Feedback has been successfully approved.", "Dismiss");
  }

  unapproveFeedback(feedback: IFeedback) {
    this._feedbackService.unapproveFeedback(feedback).subscribe(editedFeedback => {
      let index = this.allFeedbacks.indexOf(feedback);
      this.allFeedbacks[index] = editedFeedback;
    },
      error => {                             
        console.error('Error approving feedback!' + error.HttpErrorResponse)
    });
    this._snackBar.open("Feedback has been successfully approved.", "Dismiss");
  }
  
}
