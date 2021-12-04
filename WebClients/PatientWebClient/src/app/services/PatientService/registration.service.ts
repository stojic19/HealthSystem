import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { INewPatient } from 'src/app/interfaces/new-patient';

@Injectable({
  providedIn: 'root',
})
export class RegistrationService {
  constructor(private _http: HttpClient) {}

  registerPatient(newPatient: INewPatient) {
    return this._http.post('/api/Registration/Register', newPatient, {
      responseType: 'text',
    });
  }
}
