import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-time-info',
  templateUrl: './time-info.component.html',
  styleUrls: ['./time-info.component.css']
})
export class TimeInfoComponent implements OnInit {
  @Output()
  duration = new EventEmitter<number>();
  @Output()
  startDate = new EventEmitter<Date>();
  @Output()
  endDate = new EventEmitter<Date>();

  enteredStartDate: Date;
  enteredEndDate: Date;
  enteredDuration: number;
  currentDate: Date = new Date();
  errorMessage = '';

  constructor() { }

  ngOnInit(): void {
  }

  emitStartDate() {
    this.startDate.emit(this.enteredStartDate);
  }

  emitEndDate() {
    this.endDate.emit(this.enteredEndDate);
  }

  emitDuration() {
    this.duration.emit(this.enteredDuration);
  }

  isEnteredDataCorrect() {

    if (this.enteredDuration <= 0) {
      this.errorMessage = 'The value you entered must be larger than zero!';
      return false;
    }

    if (this.enteredStartDate > this.enteredEndDate) {
      this.errorMessage = "End date can't be before start!";
      return false;
    }

    this.errorMessage = '';
    return true;
  }

}