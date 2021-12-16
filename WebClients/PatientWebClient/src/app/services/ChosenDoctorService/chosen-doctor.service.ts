import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IChosenDoctor } from 'src/app/interfaces/chosen-doctor';

@Injectable({
  providedIn: 'root',
})
export class ChosenDoctorService {
  constructor(private _http: HttpClient) {}

  getAllNonLoaded(): Observable<IChosenDoctor[]> {
    return this._http.get<IChosenDoctor[]>(
      '/api/Doctor/GetNonOverloadedDoctors'
    );
  }

  getAllWithSpeciality(specId: number): Observable<IChosenDoctor[]> {
    return this._http.get<IChosenDoctor[]>(
      '/api/Doctor/GetDoctorsWithSpecialization?specializationId=' + specId
    );
  }
}
