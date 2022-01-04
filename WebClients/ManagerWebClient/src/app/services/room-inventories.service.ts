import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { AvailableTermsRequest } from '../model/available-terms-request';
import { TimePeriod } from '../model/time-period';
import { EquipmentTransferEvent } from '../model/equipment-transfer-event';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class RoomInventoriesService {
  constructor(private http: HttpClient) {}

  getRoomInventory(roomId: number) {
    return this.http.get(
      `${environment.baseHospitalUrl}` + 'api/RoomInventory/GetRoomInventory',
      {
        params: {
          roomId: roomId,
        },
      }
    );
  }

  getHospitalInventory() {
    return this.http.get(
      `${environment.baseHospitalUrl}` + 'api/RoomInventory/GetHospitalInventory'
    );
  }

  getEquipmentByName(equipmentName: string) {
    return this.http.get(
      `${environment.baseHospitalUrl}` + 'api/RoomInventory/FindByInventoryItemName',
      {
        params: {
          inventoryItemName: equipmentName,
        },
      }
    );
  }

  getItemById(itemId: number) {
    return this.http
      .get(
        `${environment.baseHospitalUrl}` + 'api/RoomInventory/GetRoomInventoryById',
        {
          params: {
            id: itemId,
          },
        }
      )
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
      `${environment.baseHospitalUrl}` + 'api/RoomInventory/GetRoomInventoryAmount',
      {
        params: {
          itemId: itemId,
          roomId: roomId,
        },
      }
    );
  }

  getAvailableTerms(request: AvailableTermsRequest) {
    return this.http.post(
      '/api/EquipmentTransferEvent/GetAvailableTerms',
      request
    );
  }

  addEquipmentTransferEvent(newTransfer: EquipmentTransferEvent) {
    console.log(newTransfer);
    return this.http
      .post(
        '/api/EquipmentTransferEvent/AddNewEquipmentTransferEvent',
        newTransfer
      )
      .subscribe();
  }
}
