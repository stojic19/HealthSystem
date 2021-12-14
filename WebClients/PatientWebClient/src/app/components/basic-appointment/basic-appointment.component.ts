import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IChosenDoctor } from 'src/app/interfaces/chosen-doctor';
import { ISpecialization } from 'src/app/interfaces/specialization';
import { AppointmentService } from 'src/app/services/AppointmentService/appointment.service';
import { ChosenDoctorService } from 'src/app/services/ChosenDoctorService/chosen-doctor.service';
import { SpecializationService } from 'src/app/services/SpecializationService/specialization.service';
import { DatePipe } from '@angular/common';
import { INewAppointment } from 'src/app/interfaces/new-appointment';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-basic-appointment',
  templateUrl: './basic-appointment.component.html',
  styleUrls: ['./basic-appointment.component.css'],
  providers: [DatePipe],
})
export class BasicAppointmentComponent implements OnInit {
  preferredDate!: string;
  doctors!: IChosenDoctor[];
  newAppointment!: INewAppointment;
  specializations!: ISpecialization[];
  availableTerms: any = [];
  firstFormGroup!: FormGroup;
  secondFormGroup!: FormGroup;
  thirdFormGroup!: FormGroup;
  fourthFormGroup!: FormGroup;
  minDate!: Date;

  constructor(
    private _formBuilder: FormBuilder,
    private doctorService: ChosenDoctorService,
    private specializationService: SpecializationService,
    private appointmentService: AppointmentService,
    private datePipe: DatePipe,
    private _snackBar: MatSnackBar,
    private router: Router
  ) {
    this.newAppointment = {} as INewAppointment;
    this.minDate = new Date();
  }

  ngOnInit(): void {
    this.firstFormGroup = this._formBuilder.group({
      start: ['', Validators.required],
    });
    this.secondFormGroup = this._formBuilder.group({
      secondCtrl: ['', Validators.required],
    });
    this.thirdFormGroup = this._formBuilder.group({
      thirdCtrl: ['', Validators.required],
    });
    this.fourthFormGroup = this._formBuilder.group({
      fourthCtrl: [''],
    });

    this.specializationService
      .getAll()
      .subscribe((res) => (this.specializations = res));
  }

  getDocsForSpec(event: any) {
    this.doctorService.getAllWithSpeciality(event.value).subscribe((res) => {
      this.doctors = res;
    });
  }

  dateChange(dateStart: HTMLInputElement) {
    this.preferredDate = dateStart.value;
  }

  getTerms(event: any) {
    this.appointmentService
      .getAvailableTerms(event.value, this.preferredDate)
      .subscribe((res) => {
        res.forEach((date) => {
          const formDat = this.datePipe.transform(
            date.toString(),
            'MMM dd, yyyy HH:mm'
          );
          this.availableTerms.push(formDat);
        });
      });
    this.newAppointment.doctorId = event.value;
  }

  termPicked(term: any) {
    const date = new Date(Date.parse(term.value));
    const dateFormated = this.datePipe.transform(date, 'yyyy-MM-ddTHH:mm:ssZ');
    this.newAppointment.startDate = dateFormated;
  }

  schedule() {
    this.newAppointment.patientId = 2;
    console.log(this.newAppointment.startDate);
    this.appointmentService.scheduleAppointment(this.newAppointment).subscribe(
      (res) => {
        this.router.navigate(['/record']);
        this._snackBar.open('Appointment successfully scheduled!', 'Dismiss');
      },
      (err) => {
        this.router.navigate(['/appointments']);
        this._snackBar.open(
          'Appointment could not be scheduled! Please try again.',
          'Dismiss'
        );
      }
    );
  }
}
