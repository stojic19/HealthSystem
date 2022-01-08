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
  shift = "";
  isAddOperation: boolean;
  isUpdateOperation : boolean;
  doctorShift = "";
  doctors : DoctorShift[];
  constructor(private service : DoctorShiftService) { }

  ngOnInit(): void {
    this.service.getDoctors().toPromise().then(res => this.doctors = res as DoctorShift[]);
    this.isLoading = false;
  }

  addShift(){
    this.isAddOperation = true;
    this.isUpdateOperation = false;
  }

  updateShift(shiftName: string){
    this.doctorShift = shiftName;
    this.isUpdateOperation = true;
    this.isAddOperation = false;
  }

}
