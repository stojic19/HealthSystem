import { HttpClient} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IAppointment } from 'src/app/interfaces/appointment';
import { Observable } from 'rxjs';
import { INewAppointment } from 'src/app/interfaces/new-appointment';

@Injectable({
  providedIn: 'root',
})
export class AppointmentService {
  constructor(private _http: HttpClient) {}

  getAvailableTerms(
    doctorId: number,
    preferredDate: string
  ): Observable<String[]> {
    return this._http.get<String[]>(
      '/api/ScheduledEvent/GetAvailableAppointments?doctorId=' +
        doctorId +
        '&preferredDate=' +
        preferredDate
    );
  }
  getAppointment(eventId:string): any {
    return this._http.get<IAppointment>('/api/ScheduledEvent/GetScheduledEvent/'+ eventId);
  }

  scheduleAppointment(newAppointment: INewAppointment): Observable<any> {
    return this._http.post<any>(
      'api/ScheduleAppointment/ScheduleAppointment',
      newAppointment
    );
  }
}
