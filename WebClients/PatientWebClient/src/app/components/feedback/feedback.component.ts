import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { IFeedback } from 'src/app/interfaces/ifeedback';
import { FeedbackService } from 'src/app/services/feedback.service';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css'],
  providers: [FeedbackService]
})
export class FeedbackComponent implements OnInit {

  newFeedback : IFeedback;  
  stayAnonymous : boolean = false;
  isPublishable : boolean = false
  feedbackText : string = '';
  
  constructor(public dialogRef: MatDialogRef<FeedbackComponent>, private _feedbackService: FeedbackService) {
    this.newFeedback = {} as IFeedback;
  }

  ngOnInit(): void {
  }
  
  doTextareaValueChange(ev : any) {
    this.feedbackText = ev.target.value;
  }

  stayAnonymousChanged() : void {
    this.stayAnonymous = !this.stayAnonymous;
  }

  isPublishableChanged() : void {
    this.isPublishable = !this.isPublishable;
  }

  submitFeedback() : void {
    this.newFeedback.text = this.feedbackText;
    this.newFeedback.isPublishable = this.isPublishable;
    !this.stayAnonymous ? this.newFeedback.patientId = 1 : true;
    this._feedbackService.addFeedback(this.newFeedback);
    this.dialogRef.close;
  }

}
