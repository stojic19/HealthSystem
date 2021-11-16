import { Component, OnInit, } from '@angular/core';
import { Router } from '@angular/router';

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
  constructor(private router: Router) { }

  ngOnInit(): void { }

  showFirstBuilding() {
    this.router.navigateByUrl("firstBuilding");
  }

  showSecondBuilding() {
    this.router.navigateByUrl("secondBuilding");
  }
}
