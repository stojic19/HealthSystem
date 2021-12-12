import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IDoctor } from 'src/app/interfaces/doctor';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {

  constructor(private _http: HttpClient) { }

  getAll(): Observable<IDoctor[]> {
    return this._http.get<IDoctor[]>(
      '/api/Doctor/GetAllDoctors'
    );
  }
}
