import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { Room } from 'src/app/interfaces/room';
import { RoomsService } from 'src/app/services/rooms.service';


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

  constructor(public service: RoomsService) {
    service.getFirstFloorOfSecondBuilding();
  }

  ngOnInit(): void {
  }

  selectRoom(room: Room) {
    this.selectedRoom.emit(room);
  }

  calculateX(room: Room) {
    return room.roomPosition.dimensionX + room.roomPosition.width / 2;
  }

  calculateY(room: Room) {
    return room.roomPosition.dimensionY + room.roomPosition.height / 2;
  }

}
