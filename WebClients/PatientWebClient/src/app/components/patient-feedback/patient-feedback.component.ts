import { IPatientFeedback, FeedbackStatus } from './../../interfaces/patient-feedback/patient-feedback-interface'
import { FeedbackService } from './../../services/FeedbackService/feedback.service';
import { Component, Input, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-patient-feedback',
  templateUrl: './patient-feedback.component.html',
  styleUrls: ['./patient-feedback.component.css']
})
export class PatientFeedbackComponent implements OnInit {

  @Input()
  allFeedbacks!: IPatientFeedback[];
  constructor() { }

  ngOnInit(): void {
  }
}
