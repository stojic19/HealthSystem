import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { StartSchedulingPerPartOfDay } from '../interfaces/start-scheduling-per-part-of-day';
import { SuccessSchedulingPerDayOfWeek } from '../interfaces/succes-scheduling-per-day-of-week';
import { NumberOfScheduling } from '../interfaces/number-of-scheduling';

@Injectable({
  providedIn: 'root'
})
export class EventsService {

  constructor(private _http: HttpClient) { }

  getStatisticsPerPartOfDay(): any{
    return this._http.get<StartSchedulingPerPartOfDay>
    ('/api/Event/GetStatisticsPerPartOfDay');
  }

  getStatisticsPerDayOfWeek(): any{
    return this._http.get<SuccessSchedulingPerDayOfWeek>
    ('/api/Event/GetStatisticsPerDayOfWeek');
  }

  getStatisticsPerMonths(): any{
    return this._http.get<number[]>
    ('/api/Event/GetStatisticsPerMonths');
  }

  getNumberOfStepsSuccessScheduling(): any{
    return this._http.get<NumberOfScheduling[]>
    ('/api/Event/GetNumberOfStepsSuccessScheduling');
  }
}
