import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MedicationReportService {
  private _APIUrl = 'https://localhost:44302/api';
  constructor(private _httpClient: HttpClient) { }
  getReport(val:any): Observable<object>{
    return this._httpClient.post<object>(this._APIUrl+'/Report/CreateConsumptionReport',val);
  }
  sendReport(val:any){
    console.log(val);
    return this._httpClient.post(this._APIUrl + '/Report/SendConsumptionReport', val);
  }
}
