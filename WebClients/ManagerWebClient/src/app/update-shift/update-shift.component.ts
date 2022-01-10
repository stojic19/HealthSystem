import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { ShiftService } from '../services/shift.service';

@Component({
  selector: 'app-update-shift',
  templateUrl: './update-shift.component.html',
  styleUrls: ['./update-shift.component.css']
})
export class UpdateShiftComponent implements OnInit {

  errorMessage = '';
  fromTime: string;
  toTime: string;
  public selectedItemId: number;
  isProd: boolean = environment.production;

  constructor(private service: ShiftService, private route: ActivatedRoute, private router: Router) {
    this.route.params.subscribe((params) => {
      this.selectedItemId = +params['id'];
    });
  }

  ngOnInit(): void {
  }

  isEnteredDataCorrect() {

    if (+this.fromTime < 0 || +this.toTime < 0) {
      this.errorMessage = "Time must be positive number!";
      return false;
    }

    if (+this.fromTime < 0 || +this.toTime < 0) {
      this.errorMessage = "Time must be positive number!";
      return false;
    }

    if (this.fromTime == '') {
      this.errorMessage = "From time must be entered!";
      return false;
    }

    if (this.toTime == '') {
      this.errorMessage = "To time must be entered!";
      return false;
    }

    this.errorMessage = '';
    return true;
  }

  isButtonDisabled() {

    if (this.fromTime == '' || this.toTime == '') {
      return true;
    }
    if (this.fromTime == undefined || this.toTime == undefined) {
      return true;
    }
    if (+this.fromTime < 0 || +this.toTime < 0) {
      return true;
    }
    return false;
  }

  updateShift() {
    this.service.updateShift(this.selectedItemId, +this.fromTime, +this.toTime);
    this.router.navigate([this.isProd ? '/manager/hospitalShifts' : '/hospitalShifts']);
  }

}
