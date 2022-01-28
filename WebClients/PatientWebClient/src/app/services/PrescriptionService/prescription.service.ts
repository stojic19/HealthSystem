import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPrescription } from 'src/app/interfaces/prescription';

@Injectable({
  providedIn: 'root',
})
export class PrescriptionService {
  constructor(private _http: HttpClient) {}

  getPrescriptionForScheduledEvent(
    seid: number,
    patientUsername: string
  ): Observable<IPrescription> {
    return this._http.get<IPrescription>(
      'api/Prescription/GetPrescriptionForScheduledEvent?scheduledEventId=' +
        seid +
        '&patientUsername=' +
        patientUsername
    );
  }
}
