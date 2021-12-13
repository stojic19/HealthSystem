import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { INewAppointment } from 'src/app/interfaces/new-appointment';

@Injectable({
  providedIn: 'root',
})
export class AppointmentService {
  constructor(private _http: HttpClient) {}

  getAvailableTerms(
    doctorId: number,
    startDate: string,
    endDate: string
  ): Observable<String[]> {
    return this._http.get<String[]>(
      '/api/ScheduledEvent/GetAvailableAppointments?doctorId=' +
        doctorId +
        '&startDate=' +
        startDate +
        '&endDate=' +
        endDate
    );
  }

  scheduleAppointment(newAppointment: INewAppointment): Observable<any> {
    return this._http.post<any>(
      'api/ScheduleAppointment/ScheduleAppointment',
      newAppointment
    );
  }
}
