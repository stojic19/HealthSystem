import { Injectable } from '@angular/core';
import { RoomInventoryComponent } from '../room-inventory/room-inventory.component';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { AvailableTermsRequest } from '../model/available-terms-request';

@Injectable({
  providedIn: 'root',
})
export class RoomInventoriesService {
  constructor(private http: HttpClient) {}

  getRoomInventory(roomId: number) {
    return this.http.get(`${environment.baseUrl}` + 'api/RoomInventory', {
      params: {
        roomId: roomId,
      },
    });
  }

  getHospitalInventory() {
    return this.http.get(
      `${environment.baseUrl}` + 'api/RoomInventory/hospitalInventory'
    );
  }

  getEquipmentByName(equipmentName: string) {
    return this.http.get(`${environment.baseUrl}` + 'api/RoomInventory/find', {
      params: {
        inventoryItemName: equipmentName,
      },
    });
  }

  getItemById(itemId: number) {
    return this.http
      .get(`${environment.baseUrl}` + 'api/RoomInventory/getById', {
        params: {
          id: itemId,
        },
      })
      .toPromise()
      .then((result) => {
        return result;
      })
      .catch((err) => {
        console.log(err);
      });
  }

  getItemAmount(roomId: number, itemId: number) {
    return this.http.get<number>(
      `${environment.baseUrl}` + 'api/RoomInventory/amount',
      {
        params: {
          itemId: itemId,
          roomId: roomId,
        },
      }
    );
  }

  getAvailableTerms(request: AvailableTermsRequest) {}
}
