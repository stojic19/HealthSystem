import { Component, OnInit } from '@angular/core';
import { Room } from '../interfaces/room';

@Component({
  selector: 'app-equipment-form',
  templateUrl: './equipment-form.component.html',
  styleUrls: ['./equipment-form.component.css']
})
export class EquipmentFormComponent implements OnInit {
  step = 1;
  initialRooms! : Room[];
  destinationRooms! : Room[];
  initialRoom! : Room;
  destinationRoom! : Room;

  constructor() { }

  ngOnInit(): void {
  }

  prevStep() {
    this.step--;
  }
  
  nextStep() {
    this.step++;
  }
}
