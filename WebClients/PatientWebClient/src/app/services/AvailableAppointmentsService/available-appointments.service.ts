import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAvailableAppointment } from 'src/app/interfaces/availableappointment';
import { IRecommendedAppointment } from 'src/app/interfaces/irecommendedappointment';

@Injectable({
  providedIn: 'root'
})
export class AvailableAppointmentsService {

  constructor(private _http: HttpClient) { }

  getAvailableAppointments(doctorId: number,dateStart: string,dateEnd:string,isDoctorPriority : boolean): Observable<IAvailableAppointment[]>{
    return this._http.get<IAvailableAppointment[]>('/api/RecommendedAppointment/GetRecommendedAppointments?doctorId='+doctorId+'&dateStart='+dateStart+'&dateEnd='+dateEnd+'&isDoctorPriority='+isDoctorPriority);
  }

  createNewAppointment(newAppointment: IRecommendedAppointment) {
    return this._http.post('/api/RecommendedAppointment/ScheduleAppointment', newAppointment, {
      responseType: 'text',
    });
  }
}
