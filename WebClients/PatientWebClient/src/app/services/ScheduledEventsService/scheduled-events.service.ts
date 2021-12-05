import { Observable } from 'rxjs';
import { IScheduledEvent } from 'src/app/interfaces/scheduled-event-interface';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ScheduledEventsService {

  constructor(private _http: HttpClient) { }

  getEventsForPatient(): Observable<IScheduledEvent[]> {
    return this._http.get<IScheduledEvent[]>('/api/ScheduledEvents/GetFinishedUserEvents/1');
  }

}
