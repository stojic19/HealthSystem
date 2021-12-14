import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { IAppointment } from 'src/app/interfaces/appointment';
import { IFinishedAppointment } from 'src/app/interfaces/finished-appoinment';
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
  
  
  futureAppointments!:IAppointment[];
  finishedAppointments!:IFinishedAppointment[];
  canceledAppointments!:IAppointment[];

  constructor(private _service: MedicalRecordService ,private _router :Router ,private _activeRoute: ActivatedRoute) {
    
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
    this.sub = this._service.getFutureAppointments().subscribe({
      next: (futureAppointments:IAppointment[])=>{
        this.futureAppointments = futureAppointments;
      }
    });
    this.sub = this._service.getfinishedAppointments().subscribe({
      next: (finishedAppointments:IFinishedAppointment[])=>{
        this.finishedAppointments = finishedAppointments;
      }
    });
    this.sub = this._service.getCanceledAppointments().subscribe({
      next: (canceledAppointments:IAppointment[])=>{
        this.canceledAppointments = canceledAppointments;
      }
    });
    
  }
  answerSurvey(id:number){
     console.log(id);
     var str = id.toString();
     this._router.navigate(['/survey',str]);
  }

}

