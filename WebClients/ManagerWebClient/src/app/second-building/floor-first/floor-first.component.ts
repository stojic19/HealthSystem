import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { Room } from 'src/app/interfaces/room';
import { RoomPosition } from 'src/app/model/room-position.model';
import { RoomPositionService } from 'src/app/services/room-position.service';


@Component({
  selector: 'app-floor-first',
  templateUrl: './floor-first.component.html',
  styleUrls: ['./floor-first.component.css']
})
export class FloorFirstComponent implements OnInit {

  public roomColor = '#fccfcf';
  public selectedRoomColor = '#90caf9';
  public doorColor = ' #808080';
  public borderColor = '#000000';
  public borderWidth = 1;
  @Output()
  selectedRoom = new EventEmitter();
  @Input() public roomForDisplay = '';

  constructor(public service: RoomPositionService) {
    service.getFirstFloorOfSecondBuilding();
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
