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

  get(userName:any): any {
    return this._http.get<IPatient>('api/MedicalRecord/GetPatientWithRecord/' + userName);
  }
  getFutureAppointments(userName:any): any {
    
    return this._http.get<IAppointment[]>('api/ScheduledEvent/GetUpcomingUserEvents/' + userName);
  }

  getfinishedAppointments(userName:any): any {
 
    return this._http.get<IFinishedAppointment[]>('api/ScheduledEvent/GetEventsForSurvey/' + userName);
  }

  getCanceledAppointments(userName:any): any {
  
    return this._http.get<IAppointment[]>('api/ScheduledEvent/GetCanceledUserEvents/' + userName);
  }
  cancelAppointments(id: number) :any {
    console.log(id);
    
    return this._http.get<any>('api/ScheduledEvent/CancelAppointment/' + id);
  }
}
