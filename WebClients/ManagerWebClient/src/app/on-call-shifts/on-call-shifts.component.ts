import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-on-call-shifts',
  templateUrl: './on-call-shifts.component.html',
  styleUrls: ['./on-call-shifts.component.css']
})
export class OnCallShiftsComponent implements OnInit {
  months: string[] = ['January', 'February', 'March', 'April', 'May', 'June', 'July',
  'August', 'September', 'October', 'November', 'December'];
  weeks: string[] = ['First', 'Second', 'Third', 'Last'];

  constructor() { }

  ngOnInit(): void {
  }

}
