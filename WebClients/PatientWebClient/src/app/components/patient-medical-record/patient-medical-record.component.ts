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
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-patient-medical-record',
  templateUrl: './patient-medical-record.component.html',
  styleUrls: ['./patient-medical-record.component.css'],
})
export class PatientMedicalRecordComponent implements OnInit {
  patient!: IPatient;
  sub!: Subscription;

  columnsToDisplayFutureAppointments: string[] = ['No.', 'Date', 'Time', 'Doctor', 'DoctorSpecialization', 'Room', 'Cancel'];
  columnsToDisplayCanceledAppointments: string[] = ['No.', 'Date', 'Time', 'Doctor', 'DoctorSpecialization', 'Room'];
  columnsToDisplayFinishedAppointments: string[] = ['No.', 'Date', 'Time', 'Doctor', 'DoctorSpecialization', 'Room', 'Survey'];

  futureAppointments!: MatTableDataSource<IAppointment>;
  finishedAppointments!: MatTableDataSource<IFinishedAppointment>;
  canceledAppointments!: MatTableDataSource<IAppointment>;
  message!: String;
  imagePath!: any;
  patientId!: number;
  constructor(
    private _sanitizer: DomSanitizer,
    private snackBar: MatSnackBar,
    private _service: MedicalRecordService,
    private _router: Router,
    private _activeRoute: ActivatedRoute
  ) {

    this.futureAppointments = new MatTableDataSource<IAppointment>();
    this.finishedAppointments = new MatTableDataSource<IFinishedAppointment>();
    this.canceledAppointments = new MatTableDataSource<IAppointment>();
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
    this.patientId = 1;
    this.sub = this._service.getFutureAppointments(this.patientId).subscribe({
      next: (futureAppointments: IAppointment[]) => {
        this.futureAppointments.data = futureAppointments;
      },
    });
    this.sub = this._service.getfinishedAppointments(this.patientId).subscribe({
      next: (finishedAppointments: IFinishedAppointment[]) => {
        this.finishedAppointments.data = finishedAppointments;
      },
    });
    this.sub = this._service.getCanceledAppointments(this.patientId).subscribe({
      next: (canceledAppointments: IAppointment[]) => {
        this.canceledAppointments.data = canceledAppointments;
      },
    });
  }
  answerSurvey(id: number) {
    console.log(id);
    var str = id.toString();
    this._router.navigate(['/survey', str]);
  }
}
