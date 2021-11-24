import { DatePipe } from '@angular/common';
import { Component, Input, Output, OnInit, EventEmitter } from '@angular/core';
import { Room } from 'src/app/interfaces/room';
import { RoomInventory } from 'src/app/model/room-inventory.model';
import { RoomInventoriesService } from 'src/app/services/room-inventories.service';

@Component({
  selector: 'app-move-info',
  templateUrl: './move-info.component.html',
  styleUrls: ['./move-info.component.css'],
})
export class MoveInfoComponent implements OnInit {
  @Input()
  selectedItem: RoomInventory;
  @Input()
  destinationRoom: Room;
  currentAmount: number;

  @Output()
  enteredAmount = new EventEmitter<number>();
  @Output()
  duration = new EventEmitter<number>();
  @Output()
  startDate = new EventEmitter<Date>();
  @Output()
  endDate = new EventEmitter<Date>();

  enteredNumber: number;
  enteredStartDate: Date;
  enteredEndDate: Date;
  enteredDuration: number;
  currentDate: Date = new Date();
  errorMessage = '';

  constructor(
    private roomInventoryService: RoomInventoriesService
  ) {}

  ngOnInit(): void {
    this.roomInventoryService
      .getItemAmount(this.destinationRoom.id, this.selectedItem.id)
      .toPromise()
      .then((res) => (this.currentAmount = res as number));
  }

  emitAmount() {
    this.enteredAmount.emit(this.enteredNumber);
  }

  emitStartDate() {
    this.startDate.emit(this.enteredStartDate);
  }

  emitEndDate() {
    this.endDate.emit(this.enteredEndDate);
  }

  emitDuration() {
    this.duration.emit(this.enteredDuration);
  }

  isEnteredDataCorrect() {
    if (this.enteredNumber > this.selectedItem.amount) {
      this.errorMessage = 'Entered amount is larger than existing!';
      return false;
    }

    if (this.enteredNumber < 0 || this.enteredDuration < 0) {
      this.errorMessage = 'The value you entered must be larger than zero!';
      return false;
    }

    if (this.enteredStartDate > this.enteredEndDate) {
      this.errorMessage = "End date can't be before start!";
      return false;
    }

    this.errorMessage = '';
    return true;
  }

  isEnteredValueCorrect() {
    if (
      (this.enteredNumber > 0 || this.enteredNumber === undefined) &&
      (this.enteredDuration > 0 || this.enteredDuration === undefined)
    )
      return true;

    return false;
  }

  isTimeIntervalCorrect() {
    if (
      this.enteredStartDate < this.enteredEndDate ||
      this.enteredStartDate === undefined ||
      this.enteredEndDate == undefined
    )
      return true;

    return false;
  }
}
