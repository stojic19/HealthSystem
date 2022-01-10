import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ISurveyStatistic } from '../interfaces/survey-statistic';

@Injectable({
  providedIn: 'root'
})
export class SurveyObserveService {

  constructor(private http: HttpClient) { }

  getSurvey(): any {

    return  this.http.get<ISurveyStatistic>('api/SurveyStatistics/GetSurveyStatistics');
  }

}
