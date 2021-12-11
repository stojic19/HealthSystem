import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Room } from 'src/app/interfaces/room';
import { RoomsService } from 'src/app/services/rooms.service';

@Component({
  selector: 'app-first-room',
  templateUrl: './first-room.component.html',
  styleUrls: ['./first-room.component.css']
})
export class FirstRoomComponent implements OnInit {

  @Input()
  public type = ""
  isLoading = true;
  public searchRoomName = '';
  rooms: Room[];
  selectedRoom = new Room();
  @Output()
  chosenRoom = new EventEmitter<Room>();

  constructor(public roomService: RoomsService) { }

  ngOnInit(): void {
    this.roomService
      .getAllRooms()
      .toPromise()
      .then((res) => {
        this.isLoading = false;
        this.rooms = res as Room[];
      });
  }

  searchRoomsByName() {
    this.roomService
      .getRoomsByNameFirstBuilding(this.searchRoomName)
      .toPromise()
      .then((res) => {
        this.rooms = res as Room[];
      });

    if (this.searchRoomName == '') {
      this.roomService
        .getAllRooms()
        .toPromise()
        .then((res) => {
          this.isLoading = false;
          this.rooms = res as Room[];
        });

    }
  }

  selectRoom(room: Room) {
    this.chosenRoom.emit(room);
    this.selectedRoom = room;
  }

}
