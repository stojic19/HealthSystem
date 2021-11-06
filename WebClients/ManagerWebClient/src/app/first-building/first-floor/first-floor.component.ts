import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-first-floor',
  templateUrl: './first-floor.component.html',
  styleUrls: ['./first-floor.component.css']
})
export class FirstFloorComponent implements OnInit {

  public roomColor='#90caf9';
  public doorColor=' #808080';
  public borderColor= '#000000';
  public borderWidth=1;
  constructor() { }

  ngOnInit(): void {
  }

}
