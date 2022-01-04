import { Injectable } from '@angular/core';
import { RoomPosition } from '../model/room-position.model';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RoomPositionService {

  public roomPositions: RoomPosition[];
  constructor(private http: HttpClient) { }

  getFirstFloorOfFirstBuilding() {
    return this.http
      .get(`${environment.baseHospitalUrl}` + 'api/RoomPosition/GetRoomsByLocation', {
        params: {
          floorNumber: 1,
          buildingName: 'Building 1',
        },
      })
      .toPromise()
      .then((res) => (this.roomPositions = res as RoomPosition[]));

  }

  getSecondFloorOfFirstBuilding() {
    return this.http
      .get(`${environment.baseHospitalUrl}` + 'api/RoomPosition/GetRoomsByLocation', {
        params: {
          floorNumber: 2,
          buildingName: 'Building 1',
        },
      })
      .toPromise()
      .then((res) => (this.roomPositions = res as RoomPosition[]));
  }

  getFirstFloorOfSecondBuilding() {
    return this.http
      .get(`${environment.baseHospitalUrl}` + 'api/RoomPosition/GetRoomsByLocation', {
        params: {
          floorNumber: 1,
          buildingName: 'Building 2',
        },
      })
      .toPromise()
      .then((res) => (this.roomPositions = res as RoomPosition[]));
  }

  getSecondFloorOfSecondBuilding() {
    return this.http
      .get(`${environment.baseHospitalUrl}` + 'api/RoomPosition/GetRoomsByLocation', {
        params: {
          floorNumber: 2,
          buildingName: 'Building 2',
        },
      })
      .toPromise()
      .then((res) => (this.roomPositions = res as RoomPosition[]));
  }
}
