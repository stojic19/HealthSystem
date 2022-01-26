import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IEvent } from 'src/app/interfaces/ievent';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  constructor(private _http: HttpClient) { }

  createNewEvent(newEvent: IEvent) {
    return this._http.post('/api/Event/AddEvent' ,newEvent, {
      responseType: 'text',
    });
  }
}
