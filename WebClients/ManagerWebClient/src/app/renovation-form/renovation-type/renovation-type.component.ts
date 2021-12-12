import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-renovation-type',
  templateUrl: './renovation-type.component.html',
  styleUrls: ['./renovation-type.component.css']
})
export class RenovationTypeComponent implements OnInit {

  @Output()
  type = new EventEmitter();
  @Input() public renovationType = ''

  constructor() { }

  ngOnInit(): void {
    this.renovationType = 'merge'
    this.type.emit(this.renovationType);
  }

  choseRenovationType(typeRenovation: string) {
    this.type.emit(typeRenovation);
  }

}
