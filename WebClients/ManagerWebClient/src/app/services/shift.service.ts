import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ShiftRequest } from '../model/shift-request.model';

@Injectable({
  providedIn: 'root'
})
export class ShiftService {

  constructor(private http: HttpClient) { }

  getHospitalShifts() {
    return this.http.get(
      'api/Shift/GetHospitalShifts'
    );
  }

  addNewShift(newShift: ShiftRequest) {
    return this.http
      .post(
        '/api/Shift/InsertShift',
        newShift
      )
      .subscribe();
  }

  updateShift(id: number, from: number, to: number) {

    return this.http.put(
      'api/Shift/UpdateShift',
      {},
      {
        params: {
          id: id,
          from: from,
          to: to,
        },
      }
    ).subscribe();
  }

  deleteShift(id: number) {
    return this.http
      .delete(
        '/api/Shift/DeleteShift',
        {
          params: {
            id: id
          }
        }
      )
      .subscribe();
  }
}
