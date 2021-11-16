import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Room } from 'src/app/interfaces/room';
import { RoomsService } from 'src/app/services/rooms.service';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent implements OnInit {

  public searchRoomName = '';
  public rooms!: Room[];
  @Output() public displayEvent = new EventEmitter();
  constructor(public roomService: RoomsService) { }

  ngOnInit(): void {

  }

  searchRoomsByName() {

    this.roomService.getRoomsByNameFirstBuilding(this.searchRoomName).toPromise().then(res => this.rooms = res as Room[]);

  }

  displayOnMap(room: Room) {
    this.displayEvent.emit(room);
  }

}
