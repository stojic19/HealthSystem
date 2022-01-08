import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
  constructor(private router: Router, private service : ShiftService) { }

  ngOnInit(): void {
    this.service.getHospitalShifts().toPromise().then(res => this.shifts = res as Shift[]);
    this.isLoading = false;
  }

  createNewShift() {
    this.router.navigate(['/createNewShift']);
  }

  updateShift(shiftId: number) {
    this.router.navigate(['/updateShift', shiftId]);
  }

  goBack(){
    this.router.navigate(['/hospitalShifts'])
  }

  deleteShift() {
    console.log("Deleted");
  }

}
