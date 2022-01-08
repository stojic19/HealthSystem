import { Component, OnInit } from '@angular/core';
import { Doctor } from '../model/doctor';
import { DoctorsScheduleService } from '../services/doctors-schedule.service';

@Component({
  selector: 'app-doctors-schedule',
  templateUrl: './doctors-schedule.component.html',
  styleUrls: ['./doctors-schedule.component.css']
})
export class DoctorsScheduleComponent implements OnInit {

  isLoading = true;
  doctors: Doctor[];

  constructor(public doctorsScheduleService: DoctorsScheduleService) { }

  ngOnInit(): void {
    this.doctorsScheduleService.getAllDoctors()
      .toPromise()
      .then((res) => {
        this.doctors = res as Doctor[];
        console.log(this.doctors[1].onCallDuties)
        this.isLoading = false;
      });;

  }

  getStartOfDuty(w, y, m) {
    let d = (1 + (w - 1) * 7); // 1st of January + 7 days for each week
    return new Date(y, m - 1, d);
  }

  getEndOfDuty(w, y, m) {
    let d = (1 + (w - 1) * 7); // 1st of January + 7 days for each week
    return new Date(y, m - 1, d + 6);
  }

}
