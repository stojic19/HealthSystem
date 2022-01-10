import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from "src/environments/environment";
import { ITenderingStatistics } from '../interfaces/tendering-statistics';

@Injectable({
  providedIn: 'root'
})
export class TenderingService {

  private _APIUrl = `${environment.baseIntegrationUrl}`;

  constructor(private _httpClient: HttpClient) { }

  createTender(val:any){
    return this._httpClient.post(this._APIUrl +'api/Tender/CreateTender',val);
  }

  getStatistics(timeRange:any): any{
    return this._httpClient.post<any>(this._APIUrl +'api/Tender/GetTenderStatistics',timeRange);
  }

}
