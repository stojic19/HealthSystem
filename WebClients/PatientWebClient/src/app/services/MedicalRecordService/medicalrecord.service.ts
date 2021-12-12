import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPatient } from 'src/app/interfaces/patient-interface';

@Injectable({
  providedIn: 'root'
})
export class MedicalRecordService {
  patient!: IPatient;

  constructor(private _http: HttpClient) {}

  get(): any {
    return this._http.get<IPatient>('api/MedicalRecord/GetPatientWithRecord');
  }
}
