import { NgAnalyzedFile } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { IFeedback } from 'src/app/interfaces/ifeedback';
import { FeedbackService } from 'src/app/services/FeedbackService/feedback.service';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css'],
  providers: [FeedbackService],
})
export class FeedbackComponent implements OnInit {
  newFeedback: IFeedback;
  stayAnonymous: boolean = false;
  isPublishable: boolean = false;
  feedbackText!: string;
  response!: any;
  error!: any;

  constructor(
    public dialogRef: MatDialogRef<FeedbackComponent>,
    private _feedbackService: FeedbackService,
    private _snackBar: MatSnackBar
  ) {
    this.newFeedback = {} as IFeedback;
  }

  ngOnInit(): void {}

  doTextareaValueChange(ev: any) {
    this.feedbackText = ev.target.value;
  }

  submitFeedback(): void {
    if (this.feedbackText && this.feedbackText != '') {
      this.newFeedback.text = this.feedbackText;
      this.newFeedback.isPublishable = this.isPublishable;
      !this.stayAnonymous
        ? (this.newFeedback.patientId = 1)
        : (this.newFeedback.patientId = 1);
      this._feedbackService.addFeedback(this.newFeedback).subscribe();
      this._snackBar.open(
        'Your feedback has been successfully submitted.',
        'Dismiss'
      );

      this.dialogRef.close(true);
    }
  }
}
