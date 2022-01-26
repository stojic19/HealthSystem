import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { DoctorVacation } from '../model/doctor-vacation.model';
import { DoctorVacationService } from '../services/doctor-vacation.service';

@Component({
  selector: 'app-doctor-vacations',
  templateUrl: './doctor-vacations.component.html',
  styleUrls: ['./doctor-vacations.component.css']
})
export class DoctorVacationsComponent implements OnInit {

  public doctors: DoctorVacation[];
  public doctorVacations: DoctorVacation[];
  public doctor: DoctorVacation;
  isProd: boolean = environment.production;

  constructor(private router: Router,
    private service: DoctorVacationService) { }

  ngOnInit(): void {
    this.service.getDoctors().toPromise().then(res => this.doctors = res as DoctorVacation[]);
  }


  findDoctor(id: number) {
    var doctorVac = new DoctorVacation();

    for (let i = 0; i < this.doctors.length; i++) {
      if (this.doctors[i].id == id) {
        doctorVac = this.doctors[i];
        return doctorVac;
      }
    }

    return doctorVac;
  }

  checkAddOperation(id: number) {

    var doctorVac = this.findDoctor(id);

    if (doctorVac.doctorSchedule.vacations.length > 0) {
      return true;
    }

    return false;
  }

  checkUpdateOrDeleteOperation(id: number) {

    var doctorVac = this.findDoctor(id);

    if (doctorVac.doctorSchedule.vacations.length == 1) {
      return false;
    }

    return true;
  }

  addVacation(doctorId: number) {
    this.router.navigate([this.isProd ? '/manager/createVacation' : '/createVacation', doctorId]);
  }

  updateVacation(doctorId: number) {
    this.router.navigate([this.isProd ? '/manager/updateVacation' : '/updateVacation', doctorId]);
  }

  goBack() {
    this.service.getDoctors().toPromise().then(res => this.doctors = res as DoctorVacation[]);
    this.router.navigate([this.isProd ? '/manager/doctorVacations' : '/doctorVacations']);
  }

  deleteVacation(id: number) {
    this.service.getDoctor(id).toPromise().then((res) => {
      this.doctorVacations = res as DoctorVacation[];
      this.copy(this.doctorVacations);
    });
    this.service.deleteVacation(id);
  }

  copy(selected: DoctorVacation[]) {
    this.doctor = selected[0];
    console.log(this.doctor);
  }


}
