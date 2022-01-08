import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DoctorShift } from 'src/app/model/doctor-shift.model';
import { Shift } from 'src/app/model/shift.model';
import { DoctorShiftService } from 'src/app/services/doctor-shift.service';
import { ShiftService } from 'src/app/services/shift.service';

@Component({
  selector: 'app-shift-update',
  templateUrl: './shift-update.component.html',
  styleUrls: ['./shift-update.component.css']
})
export class ShiftUpdateComponent implements OnInit {

  @Input() selectedDoctorShift: DoctorShift
  newShift: Shift
  errorMessage = '';
  shifts: Shift[];

  @Output() updateOperation = new EventEmitter();
  @Output() shiftUpdatedIndicator = new EventEmitter()

  constructor(private shiftService: ShiftService, private doctorShiftService: DoctorShiftService) { }

  ngOnInit(): void {
    this.shiftService.getHospitalShifts().toPromise().then(res => this.shifts = res as Shift[]);

  }

  isEnteredDataCorrect() {
    if (JSON.stringify(this.newShift) === '{}') {
      this.errorMessage = 'A shift must be entered!';
      return false;
    }

    this.errorMessage = '';
    return true;
  }

  isButtonDisabled() {

    if (this.newShift == null) {
      return true;
    }
    if (this.newShift == undefined) {
      return true;
    }
    return false;
  }

  updateDoctorShift() {

    let newShift: DoctorShift = {
      id: this.selectedDoctorShift.id,
      firstName: this.selectedDoctorShift.firstName,
      lastName: this.selectedDoctorShift.lastName,
      shift: this.newShift

    };
    this.doctorShiftService.addOrUpdateShiftToDoctor(newShift);
    this.shiftUpdatedIndicator.emit('changed');
    this.updateOperation.emit(false);
  }


}
