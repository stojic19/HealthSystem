import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IFeedback } from '../interfaces/feedback';

@Injectable()
export class FeedbacksManagerService {
  constructor(private http: HttpClient) {}

  getFeedback(): Observable<IFeedback[]> {
    return this.http.get<IFeedback[]>('/api/Feedback/GetAllFeedbacks');
  }

  approveFeedback(feedback: IFeedback): Observable<IFeedback> {
    var url = '/api/Feedback/ApproveFeedback';
    return this.http.put<any>(url, feedback);
  }

  unapproveFeedback(feedback: IFeedback): Observable<IFeedback> {
    var url = '/api/Feedback/UnapproveFeedback';
    return this.http.put<any>(url, feedback);
  }
}
