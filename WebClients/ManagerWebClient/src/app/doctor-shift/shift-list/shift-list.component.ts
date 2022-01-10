import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { DoctorShift } from 'src/app/model/doctor-shift.model';
import { Shift } from 'src/app/model/shift.model';
import { DoctorShiftService } from 'src/app/services/doctor-shift.service';
import { ShiftService } from 'src/app/services/shift.service';

@Component({
  selector: 'app-shift-list',
  templateUrl: './shift-list.component.html',
  styleUrls: ['./shift-list.component.css']
})
export class ShiftListComponent implements OnInit {

  shiftDisplay = "";
  shifts: Shift[];
  shift: Shift;
  doctors: DoctorShift[]
  @Input() doctorShift: DoctorShift
  @Output() shiftAddedIndicator = new EventEmitter()
  @Output() addOperation = new EventEmitter();


  constructor(private shiftService: ShiftService, private doctorShiftService: DoctorShiftService) { }

  ngOnInit(): void {
    this.shiftService.getHospitalShifts().toPromise().then(res => this.shifts = res as Shift[]);
  }

  chooseShift(shift: Shift) {
    this.shiftDisplay = shift.name;
    this.shift = shift;
  }

  isButtonDisabled() {

    if (this.shiftDisplay == '') {
      return true;
    }
    if (this.shiftDisplay == undefined) {
      return true;
    }
    return false;
  }

  addShiftToDoctor() {

    let newShift: DoctorShift = {
      id: this.doctorShift.id,
      firstName: this.doctorShift.firstName,
      lastName: this.doctorShift.lastName,
      shift: this.shift

    };
    this.doctorShiftService.addOrUpdateShiftToDoctor(newShift);
    this.shiftAddedIndicator.emit('changed');
    this.addOperation.emit(false);
  }

}
