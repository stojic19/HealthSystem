import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DoctorShift } from '../model/doctor-shift.model';
import { Shift } from '../model/shift.model';

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

  addOrUpdateShiftToDoctor(shift: DoctorShift) {

    return this.http.put(
      'api/Doctor/AddOrUpdateDoctorShift',
      shift
    ).subscribe();
  }
}
