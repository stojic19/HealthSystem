import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { IAppointment } from 'src/app/interfaces/appointment';
import { IFinishedAppointment } from 'src/app/interfaces/finished-appoinment';
import { IPatient } from 'src/app/interfaces/patient-interface';
import { MedicalRecordService } from 'src/app/services/MedicalRecordService/medicalrecord.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DomSanitizer } from '@angular/platform-browser';
import { MatTableDataSource } from '@angular/material/table';
import { ICurrentUser } from 'src/app/interfaces/current-user';

@Component({
  selector: 'app-patient-medical-record',
  templateUrl: './patient-medical-record.component.html',
  styleUrls: ['./patient-medical-record.component.css'],
})
export class PatientMedicalRecordComponent implements OnInit {
  patient!: IPatient;
  sub!: Subscription;
  isVisible: boolean = false;

  columnsToDisplayFutureAppointments: string[] = [
    'No.',
    'Date',
    'Time',
    'Doctor',
    'DoctorSpecialization',
    'Room',
    'Cancel',
  ];
  columnsToDisplayCanceledAppointments: string[] = [
    'No.',
    'Date',
    'Time',
    'Doctor',
    'DoctorSpecialization',
    'Room',
  ];
  columnsToDisplayFinishedAppointments: string[] = [
    'No.',
    'Date',
    'Time',
    'Doctor',
    'DoctorSpecialization',
    'Room',
    'Survey',
  ];

  futureAppointments!: MatTableDataSource<IAppointment>;
  finishedAppointments!: MatTableDataSource<IFinishedAppointment>;
  canceledAppointments!: MatTableDataSource<IAppointment>;
  message!: String;
  imagePath!: any;
  userName!: ICurrentUser;
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

    this.userName = JSON.parse(localStorage.getItem('currentUser')!);

    this.sub = this._service.get(this.userName.userName).subscribe({
      next: (patient: IPatient) => {
        this.patient = patient;
        this.imagePath = this._sanitizer.bypassSecurityTrustResourceUrl(
          patient.photoEncoded
        );
      },
    });
  }

  ngOnInit(): void {
    this.userName = JSON.parse(localStorage.getItem('currentUser')!);

    this.sub = this._service.get(this.userName.userName).subscribe({
      next: (patient: IPatient) => {
        this.patient = patient;
      },
    });

    this.sub = this._service
      .getFutureAppointments(this.userName.userName)
      .subscribe({
        next: (futureAppointments: IAppointment[]) => {
          this.futureAppointments.data = futureAppointments;
        },
      });
    this.sub = this._service
      .getfinishedAppointments(this.userName.userName)
      .subscribe({
        next: (finishedAppointments: IFinishedAppointment[]) => {
          this.finishedAppointments.data = finishedAppointments;
        },
      });
    this.sub = this._service
      .getCanceledAppointments(this.userName.userName)
      .subscribe({
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

  scheduleBasic() {
    this.isVisible = true;
  }
}
