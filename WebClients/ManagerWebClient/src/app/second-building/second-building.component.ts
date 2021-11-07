import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-second-building',
  templateUrl: './second-building.component.html',
  styleUrls: ['./second-building.component.css']
})
export class SecondBuildingComponent implements OnInit {

  public selectedFloor = 'first';
  constructor() { }

  ngOnInit(): void {
  }

}
