import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IFeedback } from "../interfaces/feedback";


@Injectable()
export class FeedbacksManagerService {

  constructor(private http: HttpClient) {}

  getFeedback() : Observable<IFeedback[]>  {

    return  this.http.get<IFeedback[]>('/api/Feedback');
  }
  
}

