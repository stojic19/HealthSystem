import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class RoomScheduleService {
  constructor(private http: HttpClient) {}

  getTransferEventsByRoom(roomId: number) {
    return this.http.get(
      `${environment.baseUrl}` +
        'api/EquipmentTransferEvent/GetTransferEventsByRoom',
      {
        params: {
          roomId: roomId,
        },
      }
    );
  }

  getRenovationsByRoom(roomId: number) {
    return this.http.get(
      `${environment.baseUrl}` + 'api/RoomRenovation/GetRenovationsByRoom',
      {
        params: {
          roomId: roomId,
        },
      }
    );
  }

  getAppointmentsByRoom(roomId: number) {
    return this.http.get(
      `${environment.baseUrl}` + 'api/Room/GetScheduledEventsByRoom',
      {
        params: {
          roomId: roomId,
        },
      }
    );
  }
}