import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-floor-first',
  templateUrl: './floor-first.component.html',
  styleUrls: ['./floor-first.component.css']
})
export class FloorFirstComponent implements OnInit {

  public roomColor='#fccfcf';
  public doorColor=' #808080';
  public borderColor= '#000000';
  public borderWidth=1;
  constructor() { }

  ngOnInit(): void {
  }

}
