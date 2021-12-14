import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-second-room-info',
  templateUrl: './second-room-info.component.html',
  styleUrls: ['./second-room-info.component.css']
})
export class SecondRoomInfoComponent implements OnInit {

  @Output()
  roomName = new EventEmitter<string>();
  @Output()
  roomDescription = new EventEmitter<string>();
  @Output()
  roomType = new EventEmitter<string>();
  @Input() public type = ""

  enteredRoomName: string;
  enteredRoomDescription: string;
  enteredRoomType: string;
  errorMessage = '';

  constructor() { }

  ngOnInit(): void {
  }

  emitRoomName() {
    this.roomName.emit(this.enteredRoomName);
  }

  emitRoomDescription() {
    this.roomDescription.emit(this.enteredRoomDescription);
  }

  emitRoomType() {
    this.roomType.emit(this.enteredRoomType);
  }

  isEnteredDataCorrect() {
    if (this.enteredRoomName == '') {
      this.errorMessage = 'A name must be entered!';
      return false;
    }

    if (this.enteredRoomDescription == '') {
      this.errorMessage = 'A description must be entered!';
      return false;
    }

    if (this.enteredRoomType == '') {
      this.errorMessage = "A room type must be chosen!";
      return false;
    }

    this.errorMessage = '';
    return true;
  }

}
