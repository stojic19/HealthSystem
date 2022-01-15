import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { VacationRequest } from '../model/vacation-request.model';
import { VacationType } from '../model/vacation.model';
import { DoctorVacationService } from '../services/doctor-vacation.service';

@Component({
  selector: 'app-create-vacation',
  templateUrl: './create-vacation.component.html',
  styleUrls: ['./create-vacation.component.css']
})
export class CreateVacationComponent implements OnInit {

  vacationType = "";
  errorMessage = '';
  fromTime: Date;
  toTime: Date;
  public selectedItemId: number;
  isProd: boolean = environment.production;

  constructor(private service: DoctorVacationService, private route: ActivatedRoute, private router: Router) {
    this.route.params.subscribe((params) => {
      this.selectedItemId = +params['id'];
    });
  }

  ngOnInit(): void {
  }

  isEnteredDataCorrect() {

    if (this.vacationType == undefined) {
      this.errorMessage = 'A type must be entered!';
      return false;
    }

    if (this.fromTime > this.toTime) {
      this.errorMessage = "From time must be before To time!";
      return false;
    }

    this.errorMessage = '';
    return true;
  }

  isButtonDisabled() {

    if (this.vacationType == undefined || this.fromTime == undefined || this.toTime == undefined) {
      return true;
    }
    if (this.fromTime > this.toTime) {
      return true;
    }
    return false;
  }

  createVacation() {
    var vacType: VacationType;
    if (this.vacationType == "SickLeave")
      vacType = VacationType.SickLeave;
    else
      vacType = VacationType.Vacation;

    let newVacation: VacationRequest = {
      type: vacType,
      startDate: this.fromTime,
      endDate: this.toTime,
      doctorId: this.selectedItemId
    };
    this.service.addNewVacation(newVacation);
    this.router.navigate([this.isProd ? '/manager/doctorVacations' : '/doctorVacations']);
  }
}
