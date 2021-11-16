import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Room } from 'src/app/interfaces/room';

@Component({
  selector: 'app-destination-room',
  templateUrl: './destination-room.component.html',
  styleUrls: ['./destination-room.component.css'],
})
export class DestinationRoomComponent implements OnInit {
  @Input()
  rooms!: Room[];
  @Output()
  selectedRoom = new EventEmitter<Room>();

  constructor() {}

  ngOnInit(): void {}

  selectDestinationRoom(room: Room) {
    this.selectedRoom.emit(room);
  }
}
