import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class RoomRenovationService {

  constructor(private http: HttpClient) { }

  getSurroundingRooms(roomId: number) {
    return this.http.get(
      `${environment.baseUrl}` + 'api/RoomRenovation/GetSurroundingRoomsForRoom',
      {
        params: {
          roomId: roomId,         
        },
      }
    );
  }
}
