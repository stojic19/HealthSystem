import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IBenefit } from 'src/app/interfaces/benefit';

@Injectable({
  providedIn: 'root'
})
export class LandingPageService {

  constructor(private _http: HttpClient) { }
  getBenefits(): Observable<IBenefit[]> {
    return this._http.get<IBenefit[]>('api/Benefit/GetBenefits');
  }
}
