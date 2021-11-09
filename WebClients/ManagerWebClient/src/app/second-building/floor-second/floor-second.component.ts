import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Room } from 'src/app/interfaces/room';
import { RoomsService } from 'src/app/services/rooms.service';

@Component({
  selector: 'app-floor-second',
  templateUrl: './floor-second.component.html',
  styleUrls: ['./floor-second.component.css']
})
export class FloorSecondComponent implements OnInit {

  public roomColor='#fccfcf';
  public doorColor=' #808080';
  public borderColor= '#000000';
  public borderWidth=1;
  @Output()
  selectedRoom = new EventEmitter();

  constructor(public service: RoomsService) { 
    this.service.getSecondFloorOfSecondBuilding();  }

  ngOnInit(): void {
  }

  selectRoom(room : Room){
    this.selectedRoom.emit(room);
  }

}
