import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Room } from '../interfaces/room';

@Injectable({
  providedIn: 'root'
})
export class RoomsService {

  constructor(private http : HttpClient) { }
  public rooms! : Room[]


  getFirstFloorOfFirstBuilding(){
    return this.http.get(`${environment.baseUrl}` + 'api/Room', {
      params: {
        floorNumber: 1,
        buildingName: 'Building 1'
      }
    }).toPromise().then(res => this.rooms = res as Room[]);

  }

  getFirstFloorOfSecondBuilding(){
    return this.http.get(`${environment.baseUrl}` + 'api/Room', {
      params: {
        floorNumber: 1,
        buildingName: 'Building 2'
      }
    }).toPromise().then(res => this.rooms = res as Room[]);
  }

  getSecondFloorOfFirstBuilding(){
    return this.http.get(`${environment.baseUrl}` + 'api/Room', {
      params: {
        floorNumber: 2,
        buildingName: 'Building 1'
      }
    }).toPromise().then(res => this.rooms = res as Room[]);
  }

  getSecondFloorOfSecondBuilding(){
    return this.http.get(`${environment.baseUrl}` + 'api/Room', {
      params: {
        floorNumber: 2,
        buildingName: 'Building 2'
      }
    }).toPromise().then(res => this.rooms = res as Room[]);
  }

  getRoomsByNameFirstBuilding(roomName: string){
    return this.http.get(`${environment.baseUrl}` + 'api/Room/find', {
      params: {
        name: roomName,
        buildingName: 'Building 1'
      }
    });
      
  }

  getRoomsByNameSecondBuilding(roomName: string){
    return this.http.get(`${environment.baseUrl}` + 'api/Room/find', {
      params: {
        name: roomName,
        buildingName: 'Building 2'
      }
    });
      
  }

  editRoom(room : Room){
    console.log('Hello' + room.name);
    this.http.put<any>('/api/Room', room).subscribe();
  }
}
