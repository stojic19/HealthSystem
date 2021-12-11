import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ScheduledEventType } from 'src/app/model/schedule';

@Component({
  selector: 'app-details-dialog',
  templateUrl: './details-dialog.component.html',
  styleUrls: ['./details-dialog.component.css'],
})
export class DetailsDialogComponent implements OnInit {
  eventType: ScheduledEventType;
  startDate: Date;
  endDate: Date;
  roomName: string;
  buildingName: string;
  patientFirstName: string;
  patientLastName: string;
  doctorFirstName: string;
  doctorLastName: string;

  constructor(
    public dialogRef: MatDialogRef<DetailsDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DetailsDialogModel
  ) {
    this.eventType = this.data.eventType;
    this.startDate = this.data.startDate;
    this.endDate = this.data.endDate;
    this.roomName = this.data.roomName;
    this.buildingName = this.data.buildingName;
    this.patientFirstName = this.data.patientFirstName;
    this.patientLastName = this.data.patientLastName;
    this.doctorFirstName = this.data.doctorFirstName;
    this.doctorLastName = this.data.doctorLastName;
  }

  ngOnInit(): void {}

  onConfirm(): void {
    this.dialogRef.close();
  }
}

export class DetailsDialogModel {
  constructor(
    public eventType: ScheduledEventType,
    public startDate: Date,
    public endDate: Date,
    public roomName: string,
    public buildingName: string,
    public patientFirstName: string,
    public patientLastName: string,
    public doctorFirstName: string,
    public doctorLastName: string
  ) {}
}
