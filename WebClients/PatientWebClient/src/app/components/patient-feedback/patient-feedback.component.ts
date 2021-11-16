import { IPatientFeedback, FeedbackStatus } from './../../interfaces/patient-feedback-interface';
import { Component, Input, OnInit } from '@angular/core';

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
