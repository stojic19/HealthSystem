import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Room } from '../interfaces/room';

@Injectable({
  providedIn: 'root'
})
export class RoomsService {

  constructor(private http : HttpClient) { }
  public rooms! : Room[]

  getFirstFloorOfFirstBuilding(){
    return this.http.get('https://localhost:44303/api/Room', {
      params: {
        floorNumber: 1,
        buildingName: 'Building 1'
      }
    }).toPromise().then(res => this.rooms = res as Room[]);

  }
  getFirstFloorOfSecondBuilding(){
    return this.http.get('https://localhost:44303/api/Room', {
      params: {
        floorNumber: 1,
        buildingName: 'Building 2'
      }
    }).toPromise().then(res => this.rooms = res as Room[]);
  }
  getSecondFloorOfFirstBuilding(){
    return this.http.get('https://localhost:44303/api/Room', {
      params: {
        floorNumber: 2,
        buildingName: 'Building 1'
      }
    }).toPromise().then(res => this.rooms = res as Room[]);
  }
  getSecondFloorOfSecondBuilding(){
    return this.http.get('https://localhost:44303/api/Room', {
      params: {
        floorNumber: 2,
        buildingName: 'Building 2'
      }
    }).toPromise().then(res => this.rooms = res as Room[]);
  }

  editRoom(room : Room){
    console.log('Hello' + room.name);
    this.http.put('https://localhost:44303/api/Room', room).subscribe();
  }
}
