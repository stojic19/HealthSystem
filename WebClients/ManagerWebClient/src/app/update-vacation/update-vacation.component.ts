import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { DoctorVacation } from '../model/doctor-vacation.model';
import { VacationRequest } from '../model/vacation-request.model';
import { Vacation, VacationType } from '../model/vacation.model';
import { DoctorVacationService } from '../services/doctor-vacation.service';

@Component({
  selector: 'app-update-vacation',
  templateUrl: './update-vacation.component.html',
  styleUrls: ['./update-vacation.component.css']
})
export class UpdateVacationComponent implements OnInit {

  doctors: DoctorVacation[];
  doctor: DoctorVacation;
  vacation: Vacation;
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
    this.service.getDoctor(this.selectedItemId).toPromise().then((res) => {
      this.doctors = res as DoctorVacation[];
      this.copy(this.doctors);
    });
  }

  copy(selected: DoctorVacation[]) {
    this.doctor = selected[0];
    console.log(this.doctor);
  }

  isEnteredDataCorrect() {

    if (this.fromTime > this.toTime) {
      this.errorMessage = "From time must be before To time!";
      return false;
    }

    this.errorMessage = '';
    return true;
  }

  isButtonDisabled() {

    if (this.fromTime == undefined || this.toTime == undefined) {
      return true;
    }
    if (this.fromTime > this.toTime) {
      return true;
    }
    return false;
  }

  updateVacation() {
    let newVacation: VacationRequest = {
      type: this.doctor.vacations[0].type,
      startDate: this.fromTime,
      endDate: this.toTime,
      doctorId: this.selectedItemId
    };
    this.service.updateVacation(newVacation);
    this.router.navigate([this.isProd ? '/manager/doctorVacations' : '/doctorVacations'])
  }
}
