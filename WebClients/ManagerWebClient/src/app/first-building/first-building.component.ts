import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-first-building',
  templateUrl: './first-building.component.html',
  styleUrls: ['./first-building.component.css']
})
export class FirstBuildingComponent implements OnInit {

  public selectedFloor='first';
  constructor() { }

  ngOnInit(): void {
  }

}
