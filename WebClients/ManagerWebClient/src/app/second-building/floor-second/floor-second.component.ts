import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-floor-second',
  templateUrl: './floor-second.component.html',
  styleUrls: ['./floor-second.component.css']
})
export class FloorSecondComponent implements OnInit {

  public roomColor='#fccfcf';
  public doorColor=' #808080';
  public borderColor= '#000000';
  public borderWidth=1;
  constructor() { }

  ngOnInit(): void {
  }

}
