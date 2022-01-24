import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { ICurrentUser } from 'src/app/interfaces/current-user';
import { IPrescription } from 'src/app/interfaces/prescription';
import { AuthService } from 'src/app/services/AuthService/auth.service';
import { PrescriptionService } from 'src/app/services/PrescriptionService/prescription.service';
import { IMedication } from 'src/app/interfaces/imedication';
import { IChosenDoctor } from 'src/app/interfaces/chosen-doctor';
import { IPatient } from 'src/app/interfaces/patient-interface';

@Component({
  selector: 'app-prescription',
  templateUrl: './prescription.component.html',
  styleUrls: ['./prescription.component.css'],
})
export class PrescriptionComponent implements OnInit {
  currentUser!: ICurrentUser;
  sub!: Subscription;
  prescription!: IPrescription;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: number,
    public dialogRef: MatDialogRef<PrescriptionComponent>,
    private authService: AuthService,
    private prescriptionService: PrescriptionService
  ) {
    this.prescription = {} as IPrescription;
    this.prescription.medication = {} as IMedication;
    this.prescription.doctorInfo = {} as IChosenDoctor;
    this.prescription.patientInfo = {} as IPatient;

    this.currentUser = JSON.parse(localStorage.getItem('currentUser')!);
    this.sub = this.prescriptionService
      .getPrescriptionForScheduledEvent(this.data, this.currentUser.userName)
      .subscribe({
        next: (res) => {
          this.prescription = res;
          console.log(this.prescription);
        },
      });
  }

  ngOnInit(): void {}
}
