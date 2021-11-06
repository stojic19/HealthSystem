import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-hospital-overview',
  templateUrl: './hospital-overview.component.html',
  styleUrls: ['./hospital-overview.component.css'],
})
export class HospitalOverviewComponent implements OnInit {
  grayColor = '#dadada';
  lightPinkColor = '#fccfcf';
  beigeColor = '#faf3dd';
  blueColor = '#90caf9';
  darkPinkColor = '#e98a8a';

  buildings = ['Building 1', 'Building 2'];
  constructor() {}

  ngOnInit(): void {}
}
