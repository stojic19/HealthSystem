import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Doctor } from '../model/doctor';
import { DoctorsReport } from '../model/doctors-report';

@Injectable({
  providedIn: 'root'
})
export class DoctorsScheduleService {

  constructor(private http: HttpClient) { }

  getAllDoctors() {
    return this.http.get('/api/Doctor/GetAllDoctorsWithShifts');
  }

  getReportInformation(id: number, start: Date, end: Date): Observable<DoctorsReport> {
    let dto = {
      doctorsId: id,
      start: start,
      end: end
    }
    return this.http.post<DoctorsReport>('/api/DoctorsReport/GetReportInformation', dto)
  }
}
