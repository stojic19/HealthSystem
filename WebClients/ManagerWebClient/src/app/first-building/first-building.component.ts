import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Room } from '../interfaces/room';

@Component({
  selector: 'app-first-building',
  templateUrl: './first-building.component.html',
  styleUrls: ['./first-building.component.css']
})
export class FirstBuildingComponent implements OnInit {

  public selectedFloor = 'first';
  public selectedRoom!: Room;
  public roomForDisplay = '';
  public changeFloor = '';
  public floor = '';

  constructor(private route: ActivatedRoute) {
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

  displayRoom(room: Room) {
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
