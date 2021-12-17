import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ISpecialization } from 'src/app/interfaces/specialization';

@Injectable({
  providedIn: 'root',
})
export class SpecializationService {
  constructor(private _http: HttpClient) {}

  getAll(): Observable<ISpecialization[]> {
    return this._http.get<ISpecialization[]>('/api/Specialization/GetAll');
  }
}
