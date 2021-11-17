import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';

@Component({
  selector: 'app-select-floor',
  templateUrl: './select-floor.component.html',
  styleUrls: ['./select-floor.component.css']
})
export class SelectFloorComponent implements OnInit {

  @Output() public selectFloor = new EventEmitter();
  @Input() public floorForDisplay = ''
  @Input() public displayFloor = 'first'
  constructor() { }

  ngOnInit(): void {
    if (this.displayFloor != '') {
      this.floorForDisplay = this.displayFloor;
    } else {
      this.floorForDisplay = 'first';
    }
  }

  floorFirst() {
    this.selectFloor.emit('first');
  }

  floorSecond() {
    this.selectFloor.emit('second');
  }

}
