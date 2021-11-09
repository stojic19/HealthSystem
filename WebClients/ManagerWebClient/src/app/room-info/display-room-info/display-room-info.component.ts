import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Room } from 'src/app/interfaces/room';


@Component({
  selector: 'app-display-room-info',
  templateUrl: './display-room-info.component.html',
  styleUrls: ['./display-room-info.component.css']
})
export class DisplayRoomInfoComponent implements OnInit {

  @Input()
  room! : Room;
  @Output()
  messageToEmit = new EventEmitter<boolean>();
  constructor() { }

  ngOnInit(): void {
  }

  startEditing() {
    this.messageToEmit.emit(true);
  }

}
