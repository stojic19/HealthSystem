import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { ShiftRequest } from '../model/shift-request.model';
import { ShiftService } from '../services/shift.service';

@Component({
  selector: 'app-create-shift',
  templateUrl: './create-shift.component.html',
  styleUrls: ['./create-shift.component.css']
})
export class CreateShiftComponent implements OnInit {

  shiftName: string;
  errorMessage = '';
  fromTime: string;
  toTime: string;
  isProd: boolean = environment.production;

  constructor(private service: ShiftService, private router: Router) { }

  ngOnInit(): void {
  }

  isEnteredDataCorrect() {
    if (this.shiftName == '') {
      this.errorMessage = 'A name must be entered!';
      return false;
    }

    if (+this.fromTime < 0 || +this.toTime < 0) {
      this.errorMessage = "Time must be positive number!";
      return false;
    }

    if (this.fromTime == '') {
      this.errorMessage = 'From time must be entered!';
      return false;
    }

    if (this.toTime == '') {
      this.errorMessage = 'To time must be entered!';
      return false;
    }

    this.errorMessage = '';
    return true;
  }

  isButtonDisabled() {

    if (this.shiftName == '' || this.fromTime == '' || this.toTime == '') {
      return true;
    }
    if (this.shiftName == undefined || this.fromTime == undefined || this.toTime == undefined) {
      return true;
    }
    if (+this.fromTime < 0 || +this.toTime < 0) {
      return true;
    }
    return false;
  }

  createShift() {
    let newShift: ShiftRequest = {
      name: this.shiftName,
      from: +this.fromTime,
      to: +this.toTime

    };
    this.service.addNewShift(newShift);
    this.router.navigate([this.isProd ? '/manager/hospitalShifts' : '/hospitalShifts'])
  }

}
