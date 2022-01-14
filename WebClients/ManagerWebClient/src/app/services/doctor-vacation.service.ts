import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DoctorVacation } from '../model/doctor-vacation.model';
import { VacationRequest } from '../model/vacation-request.model';

@Injectable({
  providedIn: 'root'
})
export class DoctorVacationService {

  constructor(private http : HttpClient) { }

  getDoctors() {
    return this.http.get(
      'api/Doctor/GetFutureVacations'
    );
  }

  addNewVacation(vacation: VacationRequest) {

    return this.http.post(
      'api/Doctor/AddVacation',
      vacation
    ).subscribe();
  }

  updateVacation(vacation: VacationRequest) {

    return this.http.put(
      'api/Doctor/UpdateVacation',
      vacation
    ).subscribe();
  }

  getDoctor(id: number) {
    return this.http.get(
      'api/Doctor/GetDoctorWithFutureVacations',
      {
        params: {
          id: id
        },
      }
    );
  }

  deleteVacation(id: number) {
    return this.http
      .delete(
        '/api/Doctor/DeleteVacation',
        {
          params: {
            id: id
          }
        }
      )
      .subscribe();
  }

}
