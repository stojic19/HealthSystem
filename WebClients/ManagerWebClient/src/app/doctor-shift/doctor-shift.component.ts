import { Component, OnInit } from '@angular/core';
import { DoctorShift } from '../model/doctor-shift.model';
import { DoctorShiftService } from '../services/doctor-shift.service';

@Component({
  selector: 'app-doctor-shift',
  templateUrl: './doctor-shift.component.html',
  styleUrls: ['./doctor-shift.component.css']
})
export class DoctorShiftComponent implements OnInit {

  isLoading = true;
  isAddOperation: boolean;
  chosenOperationAdd = false;
  chosenOperationUpdate = false;
  isUpdateOperation: boolean;
  doctorShift: DoctorShift;
  doctors: DoctorShift[];
  refresh = "";
  constructor(private service: DoctorShiftService) { }

  ngOnInit(): void {
    this.service.getDoctors().toPromise().then(res => this.doctors = res as DoctorShift[]);
    this.isLoading = false;

  }

  addShift(doctorShift: DoctorShift) {
    this.isAddOperation = true;
    this.chosenOperationAdd = true;
    this.isUpdateOperation = false;
    this.doctorShift = doctorShift;
  }

  updateShift(doctorShift: DoctorShift) {
    this.isUpdateOperation = true;
    this.chosenOperationUpdate = true;
    this.isAddOperation = false;
    this.doctorShift = doctorShift;
  }

  check() {
    if (this.refresh == 'changed') {
      this.service.getDoctors().toPromise().then(res => this.doctors = res as DoctorShift[]);
      console.log(this.doctors)

    }
    return true
  }

}
