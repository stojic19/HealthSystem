import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Doctor, Vacation } from '../model/doctor';
import { DoctorsScheduleService } from '../services/doctors-schedule.service';

@Component({
  selector: 'app-doctors-schedule',
  templateUrl: './doctors-schedule.component.html',
  styleUrls: ['./doctors-schedule.component.css']
})
export class DoctorsScheduleComponent implements OnInit {

  isLoading = true;
  selectedDoctor: Doctor;
  doctors: Doctor[];

  constructor(public doctorsScheduleService: DoctorsScheduleService) { }

  ngOnInit(): void {
    this.doctorsScheduleService.getAllDoctors()
      .toPromise()
      .then((res) => {
        this.doctors = res as Doctor[];
        console.log(this.doctors[1].vacations)
        this.isLoading = false;
      });;

  }

  chosenDoctor(doctor: Doctor) {
    this.selectedDoctor = doctor;
  }

  getStartOfDuty(w, y, m) {
    let d = (1 + (w - 1) * 7); // 1st of January + 7 days for each week
    return new Date(y, m - 1, d);
  }

  getEndOfDuty(w, y, m) {
    let d = (1 + (w - 1) * 7); // 1st of January + 7 days for each week
    return new Date(y, m - 1, d + 4);
  }

  displayOnCallDuties(w, y, m) {
    let start = this.getStartOfDuty(w, y, m);
    let end = this.getEndOfDuty(w, y, m);
    let today = new Date();
    let duties = [] as any;

    let display = formatDate(new Date(start), 'dd.MM.yyy', 'en_US') + " - "
      + formatDate(new Date(end), 'dd.MM.yyy', 'en_US');

    if (new Date(end) >= today) {
      duties.push(display);
    }

    return duties;
  }

  displayPastOnCallDuties(w, y, m) {
    let start = this.getStartOfDuty(w, y, m);
    let end = this.getEndOfDuty(w, y, m);
    let today = new Date();
    let duties = [] as any;

    let display = formatDate(new Date(start), 'dd.MM.yyy', 'en_US') + " - "
      + formatDate(new Date(end), 'dd.MM.yyy', 'en_US');

    if (new Date(end) < today) {
      duties.push(display);
    }

    return duties;
  }

  getOnCallsForDoctor(doctor: Doctor) {
    let pastDuties = [] as any
    for (let d of doctor.onCallDuties) {
      let end = this.getEndOfDuty(d.week, 2022, d.month);
      let today = new Date();
      if (new Date(end) < today) {
        pastDuties.push(d);
      }
    }

    return pastDuties;
  }

  getFutureVacations(doctor: Doctor) {
    let futureVacations = [] as any;
    for (let vacation of doctor.vacations) {
      let end = new Date(vacation.endDate)
      if (end >= new Date()) {
        futureVacations.push(vacation);
      }
    }

    return futureVacations;
  }

  getPastVacations(doctor: Doctor) {
    let pastVacations = [] as any;
    for (let vacation of doctor.vacations) {
      let end = new Date(vacation.endDate)
      if (end < new Date()) {
        pastVacations.push(vacation);
      }
    }

    return pastVacations;
  }

}
