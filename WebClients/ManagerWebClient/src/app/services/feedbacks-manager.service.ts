import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { IFeedback } from "../interfaces/feedback";


@Injectable()
export class FeedbacksManagerService {

  constructor(private http: HttpClient) {}

  getFeedback() : Observable<IFeedback[]>  {

    return  this.http.get<IFeedback[]>(`${environment.baseUrl}` + 'api/Feedback');
  }
  
  approveFeedback(feedback: IFeedback) : Observable<IFeedback>  {
    var url =  '/api/Feedback';
    return this.http.put<any>(url, feedback );
 }
 
}
