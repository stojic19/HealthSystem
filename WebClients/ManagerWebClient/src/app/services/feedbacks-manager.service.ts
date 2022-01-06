import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IFeedback } from "../interfaces/feedback";



@Injectable()
export class FeedbacksManagerService {

  constructor(private http: HttpClient) {}
  getFeedback() : Observable<IFeedback[]>  {

    return  this.http.get<IFeedback[]>('/api/Feedback');
  }
  
  approveFeedback(feedback: IFeedback) : Observable<IFeedback>  {
    var url =  '/api/Feedback/publish';
    return this.http.put<any>(url, feedback );
 }

  unapproveFeedback(feedback: IFeedback) : Observable<IFeedback>  {
    var url =  '/api/Feedback/unpublish';
    return this.http.put<any>(url, feedback, );
  }
 

}

