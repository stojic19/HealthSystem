import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { IPatient } from 'src/app/interfaces/patient-interface';
import { MedicalRecordService } from 'src/app/services/MedicalRecordService/medicalrecord.service';

@Component({
  selector: 'app-patient-medical-record',
  templateUrl: './patient-medical-record.component.html',
  styleUrls: ['./patient-medical-record.component.css']
})
export class PatientMedicalRecordComponent implements OnInit {

  patient! : IPatient;
  sub!: Subscription;
  constructor(private _service: MedicalRecordService) {
    this.sub = this._service.get().subscribe({
      next: (patient : IPatient) => {
        this.patient = patient;
      }
    });
   }

  ngOnInit(): void {
    this.sub = this._service.get().subscribe({
      next: (patient : IPatient) => {
        this.patient = patient;
      }
    });
  }
}
