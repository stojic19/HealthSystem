import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Doctor } from '../model/doctor';

@Injectable({
  providedIn: 'root'
})
export class DoctorsScheduleService {

  constructor(private http: HttpClient) { }

  getAllDoctors() {
    return this.http.get('api/Doctor/GetAllDoctorsWithShifts');
  }
}
