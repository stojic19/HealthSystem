import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';

@Component({
  selector: 'app-floor-selection',
  templateUrl: './floor-selection.component.html',
  styleUrls: ['./floor-selection.component.css']
})
export class FloorSelectionComponent implements OnInit {

  @Output() public floorSelection = new EventEmitter();
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

  firstFloor() {
    this.floorSelection.emit('first');
  }

  secondFloor() {
    this.floorSelection.emit('second');
  }

}
