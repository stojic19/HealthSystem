import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Room } from '../interfaces/room';
import { RoomsService } from '../services/rooms.service';

@Component({
  selector: 'app-second-building',
  templateUrl: './second-building.component.html',
  styleUrls: ['./second-building.component.css']
})
export class SecondBuildingComponent implements OnInit {

  public selectedFloor = 'first';
  public selectedRoom!: Room;
  public searchRoomName = '';
  public rooms!: Room[];
  public roomForDisplay = '';
  public changeFloor = '';
  public floor = '';

  constructor(public roomService: RoomsService, private route: ActivatedRoute) {
    this.route.params.subscribe(params => {
      if (params['floor'] == 1) {
        this.selectedFloor = 'first';
        this.floor = 'first';
      }

      if (params['floor'] == 2) {
        this.selectedFloor = 'second';
        this.floor = 'second';

      }

      if (params['roomName'] != '') {
        this.roomForDisplay = params['roomName'];
      }
    });
  }

  ngOnInit(): void {
    this.selectedRoom = new Room();
    this.selectedRoom.name = '';
  }

  roomSelectionChanged(room: Room) {
    this.selectedRoom = room;
  }

  searchRoomsByName() {

    this.roomService.getRoomsByNameSecondBuilding(this.searchRoomName).toPromise().then(res => this.rooms = res as Room[]);

  }

  displayOnMap(room: Room) {
    this.roomForDisplay = room.name;
    if (room.floorNumber == 1) {
      this.selectedFloor = 'first';
      this.changeFloor = 'first';
    } else {
      this.changeFloor = 'second';
      this.selectedFloor = 'second';
    }

  }

}
