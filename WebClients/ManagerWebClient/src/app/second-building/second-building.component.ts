import { Component, OnInit } from '@angular/core';
import { Room } from '../interfaces/room';

@Component({
  selector: 'app-second-building',
  templateUrl: './second-building.component.html',
  styleUrls: ['./second-building.component.css']
})
export class SecondBuildingComponent implements OnInit {

  public selectedFloor = 'first';
  public selectedRoom! : Room;

  constructor() { }

  ngOnInit(): void {
    this.selectedRoom = new Room();
    this.selectedRoom.name = '';
  }

  roomSelectionChanged(room : Room){
    this.selectedRoom = room;
  }

}
