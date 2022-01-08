import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { Shift } from '../model/shift.model';
import { ShiftService } from '../services/shift.service';

@Component({
  selector: 'app-hospital-shifts',
  templateUrl: './hospital-shifts.component.html',
  styleUrls: ['./hospital-shifts.component.css']
})
export class HospitalShiftsComponent implements OnInit {

  isLoading = true;
  public shifts: Shift[];
  isProd: boolean = environment.production;

  constructor(private router: Router, private service: ShiftService) { }

  ngOnInit(): void {
    this.service.getHospitalShifts().toPromise().then(res => this.shifts = res as Shift[]);
    this.isLoading = false;
  }

  createNewShift() {

    this.router.navigate([this.isProd ? '/manager/createNewShift' : '/createNewShift']);
  }

  updateShift(shiftId: number) {
    this.router.navigate([this.isProd ? '/manager/updateShift' : '/updateShift', shiftId]);
  }

  goBack() {
    this.service.getHospitalShifts().toPromise().then(res => this.shifts = res as Shift[]);
    this.router.navigate([this.isProd ? '/manager/hospitalShifts' : '/hospitalShifts']);
  }

  deleteShift(shiftId: number) {
    this.service.deleteShift(shiftId);
  }

}
