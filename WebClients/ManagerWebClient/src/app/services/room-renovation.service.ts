import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { RenovationTermsRequest } from '../model/renovation-terms-request.model';
import { RenovationEvent } from '../model/renovation-event.model';


@Injectable({
  providedIn: 'root'
})
export class RoomRenovationService {

  constructor(private http: HttpClient) { }

  getSurroundingRooms(roomId: number) {
    return this.http.get(
      `${environment.baseHospitalUrl}` + 'api/RoomRenovation/GetSurroundingRoomsForRoom',
      {
        params: {
          roomId: roomId,         
        },
      }
    );
  }

  getAvailableTerms(request: RenovationTermsRequest) {
    return this.http.post(
      '/api/RoomRenovation/GetAvailableTerms',
      request
    );
  }

  addRenovationEvent(newRenovation: RenovationEvent) {
    console.log(newRenovation);
    return this.http
      .post(
       '/api/RoomRenovation/AddNewRoomRenovationEvent',
        newRenovation
      )
      .subscribe();
  }

}
