import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Room, RoomType } from 'src/app/interfaces/room';
import { RoomInventory } from 'src/app/model/room-inventory.model';
import { RoomsService } from 'src/app/services/rooms.service';

@Component({
  selector: 'app-destination-room',
  templateUrl: './destination-room.component.html',
  styleUrls: ['./destination-room.component.css'],
})
export class DestinationRoomComponent implements OnInit {
  @Input()
  rooms!: Room[];
  @Input()
  selectedItem: RoomInventory;
  @Output()
  selectedRoom = new EventEmitter<Room>();
  isLoading = true;
  thisRoom = new Room();
  public searchRoomName = '';
  foundRooms: Room[];
  roomType = RoomType;

  constructor(public roomsService: RoomsService) {}

  ngOnInit(): void {
    this.roomsService
      .getAllRooms()
      .toPromise()
      .then((res) => {
        this.isLoading = false;
        this.rooms = res as Room[];
        this.foundRooms = this.rooms;
      });

    console.log(this.selectedItem.roomId);
  }

  selectDestinationRoom(room: Room) {
    this.selectedRoom.emit(room);
    this.thisRoom = room;
  }

  searchRoomsByName() {
    this.roomsService
      .getRoomsByNameFirstBuilding(this.searchRoomName)
      .toPromise()
      .then((res) => {
        this.foundRooms = res as Room[];
      });

    if (this.searchRoomName == '') this.foundRooms = this.rooms;
  }
}
