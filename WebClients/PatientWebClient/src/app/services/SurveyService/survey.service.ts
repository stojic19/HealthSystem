import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAnsweredSurvey } from 'src/app/interfaces/answered-survey';
import { ISurvey } from 'src/app/interfaces/isurvey';

@Injectable({
  providedIn: 'root'
})
export class SurveyService {
  constructor(private _http: HttpClient) { }
  answerSurvey(answeredSurvey: IAnsweredSurvey): Observable<IAnsweredSurvey> {
    return this._http.post<IAnsweredSurvey>('/api/AnsweredSurvey/CreateAnsweredSurvey', answeredSurvey);
  }
  getSurvey(): any {
    return this._http.get<ISurvey>('/api/Survey/getSurveyByCategories/1');
  }
}
