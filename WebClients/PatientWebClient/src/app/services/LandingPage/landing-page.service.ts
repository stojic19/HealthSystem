import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IBenefit } from 'src/app/interfaces/benefit';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export class LandingPageService {
  private _APIUrl = `${environment.IntegrationBaseUrl}`;
  constructor(private _http: HttpClient) { }
  getBenefits(): Observable<IBenefit[]> {
    return this._http.get<IBenefit[]>('api/Benefit/GetBenefits');
  }
  getImage(val: any){
    return this._http.post<any>(this._APIUrl + 'api/Pharmacy/GetImage', val);
}
}
