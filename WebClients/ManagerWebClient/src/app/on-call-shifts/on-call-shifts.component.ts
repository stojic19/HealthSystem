import { Component, OnInit } from '@angular/core';
import { Doctor } from '../model/doctor';
import { OnCallDuty } from '../model/on-call-duty';
import { OnCallShiftsService } from '../services/on-call-shifts.service';

@Component({
  selector: 'app-on-call-shifts',
  templateUrl: './on-call-shifts.component.html',
  styleUrls: ['./on-call-shifts.component.css']
})
export class OnCallShiftsComponent implements OnInit {
  allDoctors : Doctor[];
  selectedMonth: number;
  selectedWeek: number;
  selectedOnCallShift: OnCallDuty;
  shift: OnCallDuty;
  selectedNewDoctor : Doctor;
  selectedOldDoctor : Doctor;

  constructor(public service : OnCallShiftsService) { }

  ngOnInit(): void {
    this.selectedMonth = 1;
    this.selectedWeek = 2;

    this.service
    .getAllDoctors()
    .toPromise()
    .then((res) => {
      this.allDoctors = res as Doctor[];
    });

    this.service
    .getOnCallShift(1, 2)
    .toPromise()
    .then((res) => {
      this.shift = res as OnCallDuty;
      this.selectedOnCallShift = this.shift;
      console.log( this.selectedOnCallShift);
    });
  }

  selectionChanged(){
    this.service
    .getOnCallShift(this.selectedMonth, this.selectedWeek)
    .toPromise()
    .then((res) => {
      this.selectedOnCallShift = res as OnCallDuty;
    });
  }

  addDoctorToShift(){
    this.service.addDoctorToShift(2, this.selectedNewDoctor.id)
    .toPromise()
    .then((res) => { this.selectedOnCallShift = res as OnCallDuty});
    window.location.reload;
  }

  removeDoctorFromShift(){
    this.service.removeDoctorFromShift(this.selectedOnCallShift.id, this.selectedNewDoctor.id)
    .toPromise()
    .then((res) => { this.selectedOnCallShift = res as OnCallDuty});
    window.location.reload;
  }
}
