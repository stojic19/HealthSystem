import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MedicationReportService {
  private _APIUrl = `${environment.baseIntegrationUrl}`;
  constructor(private _httpClient: HttpClient) { }
  getReport(val:any): Observable<object>{
    return this._httpClient.post<object>(this._APIUrl+'api/Report/CreateConsumptionReport',val);
  }
  sendReport(val:any){
    console.log(val);
    return this._httpClient.post(this._APIUrl + 'api/Report/SendConsumptionReport', val);
  }
}
