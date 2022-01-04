import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Room } from 'src/app/interfaces/room';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-display-room-info',
  templateUrl: './display-room-info.component.html',
  styleUrls: ['./display-room-info.component.css'],
})
export class DisplayRoomInfoComponent implements OnInit {
  @Input()
  room!: Room;
  @Output()
  messageToEmit = new EventEmitter<boolean>();
  constructor(private router: Router) {}

  isProd: boolean = environment.production;

  ngOnInit(): void {}

  startEditing() {
    this.messageToEmit.emit(true);
  }

  showRoomInventory(idR: number) {
    this.router.navigate([this.isProd ? '/manager/roomInventory' : '/roomInventory', idR]);
  }

  showRoomSchedule(id: number) {
    this.router.navigate([this.isProd ? '/manager/schedule' : '/schedule', id]);
  }
}
