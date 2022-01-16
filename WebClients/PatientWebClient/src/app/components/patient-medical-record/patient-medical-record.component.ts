import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { IAppointment } from 'src/app/interfaces/appointment';
import { IFinishedAppointment } from 'src/app/interfaces/finished-appoinment';
import { IPatient } from 'src/app/interfaces/patient-interface';
import { MedicalRecordService } from 'src/app/services/MedicalRecordService/medicalrecord.service';
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
  currentUser!: ICurrentUser;
  response!: string;
  isVisible!: boolean;

  constructor(
    private _sanitizer: DomSanitizer,
    private _service: MedicalRecordService,
    private _router: Router,
    private changeDetectorRefs: ChangeDetectorRef
  ) {
    this.futureAppointments = new MatTableDataSource<IAppointment>();
    this.finishedAppointments = new MatTableDataSource<IFinishedAppointment>();
    this.canceledAppointments = new MatTableDataSource<IAppointment>();

    this.currentUser = JSON.parse(localStorage.getItem('currentUser')!);

    this.sub = this._service.get(this.currentUser.userName).subscribe({
      next: (patient: IPatient) => {
        this.patient = patient;
        this.imagePath = this._sanitizer.bypassSecurityTrustResourceUrl(
          patient.photoEncoded
        );
      },
    });
    this.sub = this._service.get(this.currentUser.userName).subscribe({
      next: (patient: IPatient) => {
        this.patient = patient;
      },
    });
  }

  ngOnInit(): void {
    this.refresh();
  }
  refresh() {
    this.sub = this._service
      .getFutureAppointments(this.currentUser.userName)
      .subscribe({
        next: (futureAppointments: IAppointment[]) => {
          this.futureAppointments.data = futureAppointments;
          this.changeDetectorRefs.detectChanges();
        },
      });
    this.sub = this._service
      .getfinishedAppointments(this.currentUser.userName)
      .subscribe({
        next: (finishedAppointments: IFinishedAppointment[]) => {
          this.finishedAppointments.data = finishedAppointments;
          this.changeDetectorRefs.detectChanges();
        },
      });
    this.sub = this._service
      .getCanceledAppointments(this.currentUser.userName)
      .subscribe({
        next: (canceledAppointments: IAppointment[]) => {
          this.canceledAppointments.data = canceledAppointments;
          this.changeDetectorRefs.detectChanges();
        },
      });
  }
  answerSurvey(id: number) {
    var str = id.toString();
    this._router.navigate(['/survey', str]);
    this.refresh();
  }
  cancelAppointment(id: number) {
    this.sub = this._service.cancelAppointments(id).subscribe((res: string) => {
      console.log(res);
      this.refresh();
    });
  }

  scheduleBasic() {
    this.isVisible = true;
  }

  scheduleRecommended() {
    this._router.navigate(['/recommendedAppointments']);
  }
}
