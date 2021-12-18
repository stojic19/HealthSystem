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
  getFutureAppointments(): any {
    return this._http.get<IAppointment[]>('/api/ScheduledEvent/GetUpcomingUserEvents/'+1);
  }

  getfinishedAppointments(): any {
    return this._http.get<IFinishedAppointment[]>('/api/ScheduledEvent/GetEventsForSurvey/'+1);
  }

  getCanceledAppointments(): any {
    return this._http.get<IAppointment[]>('/api/ScheduledEvent/GetCanceledUserEvents/'+1);
  }

}
