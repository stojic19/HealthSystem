import { Component, OnInit, Input, SimpleChange } from '@angular/core';
import { Room } from 'src/app/interfaces/room';


@Component({
  selector: 'app-room-info',
  templateUrl: './room-info.component.html',
  styleUrls: ['./room-info.component.css']
})
export class RoomInfoComponent implements OnInit {

  editingMode = false;
  @Input()
  room!: Room;
  constructor() { }

  ngOnInit(): void {
  }

  ngOnChanges(changes: { [property: string]: SimpleChange }) {
    let change: SimpleChange = changes['room.name'];
    this.editingMode = false;
  }

}
