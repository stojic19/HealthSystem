import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: 'root'
})
export class TenderingService {

  private _APIUrl = `${environment.baseIntegrationUrl}`;

  constructor(private _httpClient: HttpClient) { }

  createTender(val:any){
    return this._httpClient.post(this._APIUrl +'api/Tender/CreateTender',val);
}

}
