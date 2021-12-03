import { Component, Input, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { IPatientFeedback } from 'src/app/interfaces/patient-feedback-interface';
import { FeedbackService } from 'src/app/services/FeedbackService/feedback.service';
import { FeedbackComponent } from '../feedback/feedback.component';

@Component({
  selector: 'app-feedbacks-page',
  templateUrl: './feedbacks-page.component.html',
  styleUrls: ['./feedbacks-page.component.css'],
})
export class FeedbacksPageComponent implements OnInit {
  allFeedbacks!: IPatientFeedback[];

  sub!: Subscription;
  constructor(private _service: FeedbackService, public matDialog: MatDialog) {}

  ngOnInit(): void {
    this.sub = this._service.getApproved().subscribe({
      next: (feedback) => {
        this.allFeedbacks = feedback;
      },
    });
  }

  openModal() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = false;
    dialogConfig.id = 'modal-component';
    dialogConfig.height = '400px';
    dialogConfig.width = '500px';
    this.matDialog.open(FeedbackComponent, dialogConfig);
  }
}
