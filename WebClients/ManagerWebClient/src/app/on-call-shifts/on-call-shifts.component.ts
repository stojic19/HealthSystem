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
    .getOnCallShift(this.selectedMonth, this.selectedWeek)
    .toPromise()
    .then((res) => {
      this.selectedOnCallShift = res as OnCallDuty;});
  }

  selectionChanged(){
    this.service
    .getOnCallShift(this.selectedMonth, this.selectedWeek)
    .subscribe((res) => {
      this.selectedOnCallShift = res as OnCallDuty;
      this.selectedOldDoctor = new Doctor;
      this.selectedOldDoctor.firstName = ''});
  }

  addDoctorToShift(){
    this.service.addDoctorToShift(this.selectedOnCallShift.id, this.selectedNewDoctor.id)
    .toPromise()
    .then((res) => { this.selectedOnCallShift = res as OnCallDuty;
                    this.selectedNewDoctor = new Doctor;
                    this.selectedNewDoctor.firstName = ''});
    window.location.reload;
  }

  removeDoctorFromShift(){
    this.service.removeDoctorFromShift(this.selectedOnCallShift.id, this.selectedOldDoctor.id)
    .toPromise()
    .then((res) => { this.selectedOnCallShift = res as OnCallDuty;
                    this.selectedOldDoctor = new Doctor;
                    this.selectedOldDoctor.firstName = ''});
    window.location.reload;
  }

  isAlreadyOnCall(): boolean{
    let flag = false;
    this.selectedOnCallShift.doctorsOnDuty.forEach(element => {
      if(element.id === this.selectedNewDoctor.id)
        flag = true;
    });
    return flag;
  }

  isInThePast(): boolean{
    let currentDate = new Date();
    let d = (1 + (this.selectedWeek - 1) * 7); 
    let selectedDate = new Date(2022, this.selectedMonth - 1, d + 6);

    if(currentDate > selectedDate)
      return true;
      
    return false;
  }
}
