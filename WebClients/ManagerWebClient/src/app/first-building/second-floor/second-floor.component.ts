import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { Room } from 'src/app/interfaces/room';
import { RoomsService } from 'src/app/services/rooms.service';

@Component({
  selector: 'app-second-floor',
  templateUrl: './second-floor.component.html',
  styleUrls: ['./second-floor.component.css']
})
export class SecondFloorComponent implements OnInit {

  public roomColor = '#89CFF0';
  public selectedRoomColor = '#fccfcf';
  public borderColor = '#000000';
  public doorColor = ' #808080';
  public borderWidth = 1;
  @Output()
  selectedRoom = new EventEmitter();
  @Input() public roomForDisplay = '';

  constructor(public service: RoomsService) {
    this.service.getSecondFloorOfFirstBuilding();
  }

  ngOnInit(): void {
  }

  selectRoom(room: Room) {
    this.selectedRoom.emit(room);
  }

}
