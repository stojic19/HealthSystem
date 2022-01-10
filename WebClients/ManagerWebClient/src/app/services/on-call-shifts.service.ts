import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { OnCallDuty } from '../model/on-call-duty';

@Injectable({
  providedIn: 'root'
})
export class OnCallShiftsService {

  constructor(private http: HttpClient) { }

  getAllDoctors() {
    return this.http.get(
      '/api/Doctor/GetAllDoctors'
    );
  }

  getOnCallShift(month: number, week: number) : Observable<OnCallDuty> {
    return this.http.get<OnCallDuty>(
      '/api/OnCallShifts/FindByMonthAndWeek',
      {
        params: {
          month: month,
          week : week
        },
      }
    );
  }

  addDoctorToShift(shiftId: number, doctorId: number) {
    return this.http.get(
      '/api/OnCallShifts/AddDoctor',
      {
        params: {
          shiftId: shiftId,
          doctorId : doctorId
        },
      }
    );
  }

  removeDoctorFromShift(shiftId: number, doctorId: number) {
    return this.http.get(
      '/api/OnCallShifts/RemoveDoctor',
      {
        params: {
          shiftId: shiftId,
          doctorId : doctorId
        },
      }
    );
  }

}
