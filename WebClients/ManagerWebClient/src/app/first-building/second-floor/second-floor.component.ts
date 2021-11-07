import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-second-floor',
  templateUrl: './second-floor.component.html',
  styleUrls: ['./second-floor.component.css']
})
export class SecondFloorComponent implements OnInit {

  public roomColor='#89CFF0';
  public borderColor= '#000000';
  public doorColor=' #808080';
  public borderWidth=1;
  constructor() { }

  ngOnInit(): void {
  }

}
