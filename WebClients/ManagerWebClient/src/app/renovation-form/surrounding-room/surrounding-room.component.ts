import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Room } from 'src/app/interfaces/room';
import { RoomPosition } from 'src/app/model/room-position.model';
import { RoomRenovationService } from 'src/app/services/room-renovation.service';

@Component({
  selector: 'app-surrounding-room',
  templateUrl: './surrounding-room.component.html',
  styleUrls: ['./surrounding-room.component.css']
})
export class SurroundingRoomComponent implements OnInit {

  @Input() public firstRoom: Room
  isLoading = true;
  rooms: Room[]
  selectedRoom = new Room();
  @Output()
  chosenSurroundingRoom = new EventEmitter<Room>();

  constructor(public service: RoomRenovationService) { }

  ngOnInit(): void {
    this.service
      .getSurroundingRooms(this.firstRoom.id)
      .toPromise()
      .then((res) => {
        this.isLoading = false;
        this.rooms = res as Room[];
      });
  }

  selectRoom(room: Room) {
    this.chosenSurroundingRoom.emit(room);
    this.selectedRoom = room;
  }

}
