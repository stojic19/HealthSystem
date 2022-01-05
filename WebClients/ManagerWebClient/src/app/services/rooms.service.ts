import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Room } from '../interfaces/room';
import { RenovationTermsRequest } from '../model/renovation-terms-request.model';

@Injectable({
  providedIn: 'root',
})
export class RoomsService {
  constructor(private http: HttpClient) { }

  getRoomsByNameFirstBuilding(roomName: string) {
    return this.http.get(
      `${environment.baseHospitalUrl}` + 'api/Room/FindByNameAndBuildingName',
      {
        params: {
          name: roomName,
          buildingName: 'Building 1',
        },
      }
    );
  }

  getRoomsByNameSecondBuilding(roomName: string) {
    return this.http.get(
      `${environment.baseHospitalUrl}` + 'api/Room/FindByNameAndBuildingName',
      {
        params: {
          name: roomName,
          buildingName: 'Building 2',
        },
      }
    );
  }

  editRoom(room: Room) {
    console.log('Hello' + room.name);
    this.http.put<any>('/api/Room', room).subscribe();
  }

  getAllRooms() {
    return this.http.get(`${environment.baseHospitalUrl}` + 'api/Room/GetAllRooms');
  }

}
