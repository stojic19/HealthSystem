import { Component, Input, OnInit } from '@angular/core';
import { RoomInventory } from 'src/app/model/room-inventory.model';
@Component({
  selector: 'app-initial-room',
  templateUrl: './initial-room.component.html',
  styleUrls: ['./initial-room.component.css'],
})
export class InitialRoomComponent implements OnInit {
  @Input()
  selectedItem: RoomInventory;

  constructor() {}

  ngOnInit(): void {}
}
