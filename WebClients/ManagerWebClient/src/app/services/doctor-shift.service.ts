import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DoctorShiftService {

  constructor(private http: HttpClient) { }

  getDoctors() {
    return this.http.get(
      'api/Doctor/GetDoctorsWithShift'
    );
  }
}
