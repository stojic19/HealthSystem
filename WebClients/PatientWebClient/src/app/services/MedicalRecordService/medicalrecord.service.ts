import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IAppointment } from 'src/app/interfaces/appointment';
import { IFinishedAppointment } from 'src/app/interfaces/finished-appoinment';
import { IPatient } from 'src/app/interfaces/patient-interface';

@Injectable({
  providedIn: 'root'
})
export class MedicalRecordService {
  patient!: IPatient;

  constructor(private _http: HttpClient) {}

  get(): any {
    return this._http.get<IPatient>('api/MedicalRecord/GetPatientWithRecord');
  }
  getFutureAppointments(patientId:number): any {
    console.log(patientId);
    
    return this._http.get<IAppointment[]>('/api/ScheduledEvent/GetUpcomingUserEvents/'+patientId);
  }

  getfinishedAppointments(patientId:number): any {
    console.log(patientId);
    return this._http.get<IFinishedAppointment[]>('/api/ScheduledEvent/GetEventsForSurvey/'+patientId);
  }

  getCanceledAppointments(patientId:number): any {
    console.log(patientId);
    return this._http.get<IAppointment[]>('/api/ScheduledEvent/GetCanceledUserEvents/'+patientId);
  }

}
