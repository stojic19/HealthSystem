import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BenefitsService {

  private _APIUrl = `${environment.baseIntegrationUrl}`;  //uneti url za http get zahtev!
    
  constructor(private _httpClient: HttpClient) {}

  getVisibleBenefits(): Observable<any[]>{
    return this._httpClient.get<any[]>(this._APIUrl+'api/Benefit/GetVisibleBenefits');
  }

  getBenefitById(id: number): Observable<any[]>{
    return this._httpClient.get<any[]>(this._APIUrl+`api/Benefit/GetBenefitById/${id}`);
  }

  publishBenefit(id: any){
    return this._httpClient.post(this._APIUrl+'api/Benefit/PublishBenefit', id);
  }

  hideBenefit(id: any){
    return this._httpClient.post(this._APIUrl+'/Benefit/HideBenefit', id);
  }
}
