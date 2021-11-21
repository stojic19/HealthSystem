import { Injectable } from '@angular/core';
import { RoomInventoryComponent } from '../room-inventory/room-inventory.component';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RoomInventoriesService {

  constructor(private http: HttpClient) { }

  getRoomInventory(roomId: number) {
    return this.http.get(`${environment.baseUrl}` + 'api/RoomInventory', {
      params: {
        roomId: roomId
      }
    });

  }

  getHospitalInventory() {

    return this.http.get(`${environment.baseUrl}` + 'api/RoomInventory/hospitalInventory');

  }

  getEquipmentByName(equipmentName: string) {
    return this.http.get(`${environment.baseUrl}` + 'api/RoomInventory/find', {
      params: {
        inventoryItemName: equipmentName

      }
    });

  }






}
