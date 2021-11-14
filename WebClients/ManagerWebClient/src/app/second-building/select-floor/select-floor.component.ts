import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';

@Component({
  selector: 'app-select-floor',
  templateUrl: './select-floor.component.html',
  styleUrls: ['./select-floor.component.css']
})
export class SelectFloorComponent implements OnInit {

  @Output() public selectFloor = new EventEmitter();
  @Input() public floorForDisplay = ''
  constructor() { }

  ngOnInit(): void {
    this.floorForDisplay = 'first';
  }

  floorFirst() {
    this.selectFloor.emit('first');
  }

  floorSecond() {
    this.selectFloor.emit('second');
  }

}
