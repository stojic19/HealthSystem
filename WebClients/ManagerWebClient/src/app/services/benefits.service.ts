import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BenefitsService {

  private _APIUrl = 'http://localhost:5000/api';  //uneti url za http get zahtev!
    
  constructor(private _httpClient: HttpClient) {}

  getVisibleBenefits(): Observable<any[]>{
    return this._httpClient.get<any[]>(this._APIUrl+'/Benefit/GetVisibleBenefits');
  }

  getBenefitById(id: number): Observable<any[]>{
    return this._httpClient.get<any[]>(this._APIUrl+`/Benefit/GetBenefitById/${id}`);
  }

  publishBenefit(id: any){
    return this._httpClient.post(this._APIUrl+'/Benefit/PublishBenefit', id);
  }

  hideBenefit(id: any){
    return this._httpClient.post(this._APIUrl+'/Benefit/HideBenefit', id);
  }
}
