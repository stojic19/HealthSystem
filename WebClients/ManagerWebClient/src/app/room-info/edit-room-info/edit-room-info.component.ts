import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Room } from 'src/app/interfaces/room';
import { RoomsService } from 'src/app/services/rooms.service';

@Component({
  selector: 'app-edit-room-info',
  templateUrl: './edit-room-info.component.html',
  styleUrls: ['./edit-room-info.component.css']
})
export class EditRoomInfoComponent implements OnInit {

  public updatedRoom!: Room;
  @Input()
  room!: Room;
  @Output()
  messageToEmit = new EventEmitter<boolean>();

  constructor(public service: RoomsService) {
  }

  ngOnInit(): void {
    this.updatedRoom = this.room;
  }

  cancelEditing() {
    this.messageToEmit.emit(false);
  }

  saveChanges() {
    this.messageToEmit.emit(false);
    this.service.editRoom(this.updatedRoom);
  }

}
