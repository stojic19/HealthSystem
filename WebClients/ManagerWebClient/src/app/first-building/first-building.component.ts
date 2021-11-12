import { Component, OnInit } from '@angular/core';
import { Room } from '../interfaces/room';

@Component({
  selector: 'app-first-building',
  templateUrl: './first-building.component.html',
  styleUrls: ['./first-building.component.css']
})
export class FirstBuildingComponent implements OnInit {

  public selectedFloor='first';
  public selectedRoom! : Room;
  public roomForDisplay='';

  constructor() { }

  ngOnInit(): void {
    this.selectedRoom = new Room();
    this.selectedRoom.name = '';
  }
  
  roomSelectionChanged(room : Room){
    this.selectedRoom = room;
  }

}
