import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RoomInventory } from '../model/room-inventory.model';
import { RoomInventoriesService } from '../services/room-inventories.service';

@Component({
  selector: 'app-hospital-equipment',
  templateUrl: './hospital-equipment.component.html',
  styleUrls: ['./hospital-equipment.component.css']
})
export class HospitalEquipmentComponent implements OnInit {

  public hospitalInventories: RoomInventory[];
  public searchEquipmentName = '';
  constructor(public roomInventoryService: RoomInventoriesService, private router : Router) { }
  selectedItem = new RoomInventory();

  ngOnInit(): void {

    this.roomInventoryService.getHospitalInventory().toPromise().then(res => this.hospitalInventories = res as RoomInventory[]);
  }

  searchEquipmentByName() {

    if (this.searchEquipmentName == '') {
      this.roomInventoryService.getHospitalInventory().toPromise().then(res => this.hospitalInventories = res as RoomInventory[]);
    } else {
      this.roomInventoryService.getEquipmentByName(this.searchEquipmentName).toPromise().then(res => this.hospitalInventories = res as RoomInventory[]);
    }
  }

  showRoom() {
    if(this.selectedItem.room.buildingName == 'Building 1')
    {
      console.log(this.selectedItem.room.floorNumber);
      this.router.navigate(['/firstBuilding', this.selectedItem.room.name, this.selectedItem.room.floorNumber]);
    } else {
      this.router.navigate(['/secondBuilding', this.selectedItem.room.name, this.selectedItem.room.floorNumber]); 
    }
  }

  selectInventoryItem(item: RoomInventory) {
    this.selectedItem = item;
  }

}
