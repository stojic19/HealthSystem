import { HttpClient} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IAppointment } from 'src/app/interfaces/appointment';

@Injectable({
  providedIn: 'root'
})
export class AppointmentService {

  constructor(private _http: HttpClient) { }

  getAppointment(eventId:string): any {
    return this._http.get<IAppointment>('/api/ScheduledEvents/GetScheduledEvent/'+ eventId);
  }
}
