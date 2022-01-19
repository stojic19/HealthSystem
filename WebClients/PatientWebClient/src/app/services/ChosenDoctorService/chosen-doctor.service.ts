import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IChosenDoctor } from 'src/app/interfaces/chosen-doctor';
import { ISpecialization } from 'src/app/interfaces/specialization';
import { SpecializationService } from '../SpecializationService/specialization.service';

@Injectable({
  providedIn: 'root',
})
export class ChosenDoctorService {
  constructor(private _http: HttpClient) {}

  getAllNonLoaded(): Observable<IChosenDoctor[]> {
    return this._http.get<IChosenDoctor[]>('/api/Doctor');
  }

  getAllWithSpeciality(specId: number): Observable<IChosenDoctor[]> {
    return this._http.get<IChosenDoctor[]>(
      '/api/Doctor/GetDoctorsWithSpecialization?specializationId=' + specId
    );
  }

  getAllSpecializations(): Observable<ISpecialization[]> {
    return this._http.get<ISpecialization[]>(
      'api/Doctor/GetAllSpecializations'
    );
  }

  getAllWithSpeciality(specName: string): Observable<IChosenDoctor[]> {
    return this._http.get<IChosenDoctor[]>(
      '/api/Doctor/GetDoctorsWithSpecialization?specializationName=' + specName
    );
  }
}
