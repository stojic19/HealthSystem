import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IFeedback } from './../../interfaces/patient-feedback/ifeedback';
import { IPatientFeedback } from 'src/app/interfaces/patient-feedback/patient-feedback-interface';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {


  constructor(private _http: HttpClient) { }

  getAll(): Observable<IPatientFeedback[]> {
    return this._http.get<IPatientFeedback[]>(`${environment.baseUrl}` + 'api/Feedback');
  }
  getApproved(): Observable<IPatientFeedback[]> {
    return this._http.get<IPatientFeedback[]>(`${environment.baseUrl}` + '/api/Feedback/approved');
  }

  addFeedback(newFeedback: IFeedback): Observable<String> {
    return this._http.post<String>('/api/Feedback', newFeedback);
  }
}
