import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IFeedback } from '../interfaces/ifeedback';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

  constructor(private http : HttpClient) { }

  addFeedback(newFeedback : IFeedback) : void {
    this.http.post<any>('/api/Feedback', newFeedback);
  }

}
