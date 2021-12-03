import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICity } from 'src/app/interfaces/city';

@Injectable({
  providedIn: 'root',
})
export class CityService {
  constructor(private _http: HttpClient) {}

  getAll(): Observable<ICity[]> {
    return this._http.get<ICity[]>('/api/City/GetAll');
  }
}
