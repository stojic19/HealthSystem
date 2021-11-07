import { Component, OnInit, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-select-floor',
  templateUrl: './select-floor.component.html',
  styleUrls: ['./select-floor.component.css']
})
export class SelectFloorComponent implements OnInit {

  @Output() public selectFloor = new EventEmitter();
  constructor() { }

  ngOnInit(): void {
  }

  floorFirst(){
    this.selectFloor.emit('first');
  }
  floorSecond(){   
    this.selectFloor.emit('second');   
  }

}
