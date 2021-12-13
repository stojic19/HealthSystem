import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { Room } from 'src/app/interfaces/room';
import { RoomPosition } from 'src/app/model/room-position.model';
import { RoomPositionService } from 'src/app/services/room-position.service';


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

  constructor(public service: RoomPositionService) {
    this.service.getSecondFloorOfFirstBuilding();
  }

  ngOnInit(): void {
  }

  selectRoom(room: Room) {
    this.selectedRoom.emit(room);
  }

  calculateX(roomPosition: RoomPosition) {
    return roomPosition.dimensionX + roomPosition.width / 2;
  }

  calculateY(roomPosition: RoomPosition) {
    return roomPosition.dimensionY + roomPosition.height / 2;
  }

}
