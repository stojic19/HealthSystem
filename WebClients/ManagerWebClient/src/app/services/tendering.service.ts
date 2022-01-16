import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from "src/environments/environment";
import { ITenderingStatistics } from '../interfaces/tendering-statistics';
import { throwError } from 'rxjs';

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

  openStatisticsPdf(fileName: string): void
  {
    this._httpClient.post(this._APIUrl+'api/Tender/getStatisticsPdf?fileName=' + fileName, {location: "report.pdf"}, { responseType: 'blob' }).subscribe(
      (response) => {
          var blob = new Blob([response], { type: "application/pdf" });
          window.open(URL.createObjectURL(blob), "_blank")
      },
      e => { throwError(e); }
    );
  }

}
