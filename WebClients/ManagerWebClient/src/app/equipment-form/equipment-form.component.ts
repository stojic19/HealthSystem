import { DatePipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Room } from '../interfaces/room';
import { AvailableTermsRequest } from '../model/available-terms-request';
import { EquipmentTransferEvent } from '../model/equipment-transfer-event';
import { RoomInventory } from '../model/room-inventory.model';
import { TimePeriod } from '../model/time-period';
import { RoomInventoriesService } from '../services/room-inventories.service';

@Component({
  selector: 'app-equipment-form',
  templateUrl: './equipment-form.component.html',
  styleUrls: ['./equipment-form.component.css'],
})
export class EquipmentFormComponent implements OnInit {
  step = 1;
  destinationRooms!: Room[];
  destinationRoom!: Room;
  public selectedItemId: number;
  public item: RoomInventory[];
  selectedItem: RoomInventory;

  enteredAmount: number;
  duration: number;
  startDate: Date;
  endDate: Date;

  availableTerms: TimePeriod[];
  terms: TimePeriod[];
  selectedTerm: TimePeriod;

  constructor(
    public roomInventoryService: RoomInventoriesService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.route.params.subscribe((params) => {
      this.selectedItemId = +params['id'];
    });
  }

  ngOnInit(): void {
    this.roomInventoryService.getItemById(this.selectedItemId).then((res) => {
      this.item = res as RoomInventory[];
      this.copy(this.item);
    });
  }

  copy(selected: RoomInventory[]) {
    this.selectedItem = selected[0];
    console.log(this.selectedItem);
  }

  prevStep() {
    if (this.step == 2 || this.step == 3) this.destinationRoom = new Room();
    this.step--;
  }

  nextStep() {
    if (this.step == 3) {
      this.getAvailableTerms();
    }

    this.step++;
  }

  isButtonDisabled() {
    if (
      (JSON.stringify(this.destinationRoom) === '{}' ||
        this.destinationRoom == null ||
        this.destinationRoom == undefined) &&
      this.step == 2
    )
      return true;

    if (this.step === 3) {
      if (
        this.enteredAmount === undefined ||
        this.startDate === undefined ||
        this.endDate === undefined ||
        this.duration === undefined
      )
        return true;
    }
    if (this.step == 4) {
      if (this.selectedTerm === undefined) return true;
    }

    return false;
  }

  getAvailableTerms() {
    let request: AvailableTermsRequest = {
      startDate: this.startDate,
      endDate: this.endDate,
      duration: this.duration,
      roomId: this.destinationRoom.id,
    };

    this.roomInventoryService.getAvailableTerms(request);
  }

  createTransferRequest() {
    let request: EquipmentTransferEvent = {
      startDate: this.selectedTerm.startDate,
      endDate: this.selectedTerm.endDate,
      initalRoomId: this.selectedItem.roomId,
      destinationRoomId: this.destinationRoom.id,
      inventoryItemId: this.selectedItem.inventoryItemId,
      quantity: this.enteredAmount,
    };
    this.roomInventoryService.addEquipmentTransferEvent(request);
    //this.router.navigate(['/hospitalEquipment']);
  }
  goBack() {
    this.router.navigate(['/hospitalEquipment']);
  }
}
