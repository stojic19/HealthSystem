import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EquipmentTransferEventDTO } from '../model/equipment-transfer-event';
import { RoomRenovationEvent } from '../model/room-renovation-event';

@Injectable({
  providedIn: 'root',
})
export class RoomScheduleService {
  constructor(private http: HttpClient) {}

  getTransferEventsByRoom(roomId: number) {
    return this.http.get(
      `${environment.baseHospitalUrl}` +
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
      `${environment.baseHospitalUrl}` + 'api/RoomRenovation/GetRenovationsByRoom',
      {
        params: {
          roomId: roomId,
        },
      }
    );
  }

  getAppointmentsByRoom(roomId: number) {
    return this.http.get(
      `${environment.baseHospitalUrl}` + 'api/Room/GetScheduledEventsByRoom',
      {
        params: {
          roomId: roomId,
        },
      }
    );
  }

  cancelEquipmentTransferEvent(transfer: EquipmentTransferEventDTO) {
    return this.http
      .post(
        '/api/EquipmentTransferEvent/CancelEquipmentTransferEvent',
        transfer
      )
      .subscribe();
  }

  cancelRenovationEvent(renovation: RoomRenovationEvent) {
    return this.http
      .post('/api/RoomRenovation/CancelRenovation', renovation)
      .subscribe();
  }
}
