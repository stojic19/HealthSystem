import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IMaliciousPatient } from '../interfaces/malicious-patient';

@Injectable({
  providedIn: 'root'
})
export class MaliciousPatientsService {
  blockPatient(patient: IMaliciousPatient) {
    return  this.http.put<IMaliciousPatient[]>('/api/BlockPatient/BlockPatient',patient);
  
  }

  constructor(private http: HttpClient) { }
  getMaliciousPatients() : Observable<IMaliciousPatient[]>  {

    return  this.http.get<IMaliciousPatient[]>('/api/BlockPatient/GetMaliciousPatients');
  }
}
