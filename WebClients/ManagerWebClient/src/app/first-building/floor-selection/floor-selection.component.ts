import { Component, OnInit, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-floor-selection',
  templateUrl: './floor-selection.component.html',
  styleUrls: ['./floor-selection.component.css']
})
export class FloorSelectionComponent implements OnInit {

  @Output() public floorSelection = new EventEmitter();
  constructor() { }

  ngOnInit(): void {
  }

  firstFloor(){
    this.floorSelection.emit('first');
  }
  secondFloor(){   
    this.floorSelection.emit('second');   
  }

}
