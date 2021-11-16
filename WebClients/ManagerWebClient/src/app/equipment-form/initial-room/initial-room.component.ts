import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Room } from 'src/app/interfaces/room';

@Component({
  selector: 'app-initial-room',
  templateUrl: './initial-room.component.html',
  styleUrls: ['./initial-room.component.css']
})
export class InitialRoomComponent implements OnInit {
  @Input()
  rooms! : Room[]; 
  @Output()
  selectedRoom = new EventEmitter<Room>();

  constructor() { }

  ngOnInit(): void {
  }

  selectInitialRoom(room : Room) {
    this.selectedRoom.emit(room);
  }

}
