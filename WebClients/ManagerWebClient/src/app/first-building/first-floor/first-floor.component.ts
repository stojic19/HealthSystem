import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Room } from 'src/app/interfaces/room';
import { RoomsService } from 'src/app/services/rooms.service';

@Component({
  selector: 'app-first-floor',
  templateUrl: './first-floor.component.html',
  styleUrls: ['./first-floor.component.css']
})
export class FirstFloorComponent implements OnInit {

  public roomColor='#90caf9';
  public doorColor=' #808080';
  public borderColor= '#000000';
  public borderWidth=1;

  @Output()
  selectedRoom = new EventEmitter();

  constructor(public service: RoomsService) {
    this.service.getFirstFloorOfFirstBuilding();
  }

  ngOnInit(): void {
  }

  selectRoom(room : Room){
    this.selectedRoom.emit(room);
  }

}
