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
}
