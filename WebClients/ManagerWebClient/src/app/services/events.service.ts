import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { StartSchedulingPerPartOfDay } from '../interfaces/start-scheduling-per-part-of-day';

@Injectable({
  providedIn: 'root'
})
export class EventsService {

  constructor(private _http: HttpClient) { }

  getStatisticsPerPartOfDay(): any{
    return this._http.get<StartSchedulingPerPartOfDay>
    ('/api/Event/GetStatisticsPerPartOfDay');
  }
}
