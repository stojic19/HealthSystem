import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { IAppointment } from 'src/app/interfaces/appointment';
import { IFinishedAppointment } from 'src/app/interfaces/finished-appoinment';
import { MedicalRecordService } from 'src/app/services/MedicalRecordService/medicalrecord.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DomSanitizer } from '@angular/platform-browser';
import { AsyncKeyword } from 'typescript';
import { IPatient } from 'src/app/interfaces/patient-feedback/patient-interface';

@Component({
  selector: 'app-patient-medical-record',
  templateUrl: './patient-medical-record.component.html',
  styleUrls: ['./patient-medical-record.component.css'],
})
export class PatientMedicalRecordComponent implements OnInit {
  patient!: IPatient;
  sub!: Subscription;

  futureAppointments!: IAppointment[];
  finishedAppointments!: IFinishedAppointment[];
  canceledAppointments!: IAppointment[];
  message!: String;
  imagePath!: any;
  constructor(
    private _sanitizer: DomSanitizer,
    private snackBar: MatSnackBar,
    private _service: MedicalRecordService,
    private _router: Router,
    private _activeRoute: ActivatedRoute
  ) {
    this.sub = this._service.get().subscribe({
      next: (patient: IPatient) => {
        this.patient = patient;
        this.imagePath = this._sanitizer.bypassSecurityTrustResourceUrl(
          patient.photoEncoded
        );
      },
    });
  }

  ngOnInit(): void {
    this.sub = this._service.get().subscribe({
      next: (patient: IPatient) => {
        this.patient = patient;
      },
    });
    this.sub = this._service.getFutureAppointments().subscribe({
      next: (futureAppointments: IAppointment[]) => {
        this.futureAppointments = futureAppointments;
      },
    });
    this.sub = this._service.getfinishedAppointments().subscribe({
      next: (finishedAppointments: IFinishedAppointment[]) => {
        this.finishedAppointments = finishedAppointments;
      },
    });
    this.sub = this._service.getCanceledAppointments().subscribe({
      next: (canceledAppointments: IAppointment[]) => {
        this.canceledAppointments = canceledAppointments;
      },
    });
  }
}
