import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IChosenDoctor } from 'src/app/interfaces/chosen-doctor';
import { ISpecialization } from 'src/app/interfaces/specialization';
import { AppointmentService } from 'src/app/services/AppointmentService/appointment.service';
import { ChosenDoctorService } from 'src/app/services/ChosenDoctorService/chosen-doctor.service';
import { DatePipe } from '@angular/common';
import { INewAppointment } from 'src/app/interfaces/new-appointment';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/AuthService/auth.service';
import { EventService } from 'src/app/services/EventSourcingService/event.service';
import { IEvent, Step } from 'src/app/interfaces/ievent';

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
  roomId!: number;
  event! : IEvent;

  constructor(
    private _formBuilder: FormBuilder,
    private doctorService: ChosenDoctorService,
    private appointmentService: AppointmentService,
    private datePipe: DatePipe,
    private _snackBar: MatSnackBar,
    private router: Router,
    private _authService: AuthService,
    private _eventService : EventService
  ) {
    this.newAppointment = {} as INewAppointment;
    this.minDate = new Date();
    this.event = {} as IEvent;
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

    this.doctorService
      .getAllSpecializations()
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
    const parts = event.value.split(',');
    this.newAppointment.doctorId = parts[0];
    this.newAppointment.doctorsRoomId = parts[1];
    this.appointmentService
      .getAvailableTerms(parts[0], this.preferredDate)
      .subscribe((res) => {
        res.forEach((date) => {
          const formDat = this.datePipe.transform(
            date.toString(),
            'MMM dd, yyyy HH:mm'
          );
          this.availableTerms.push(formDat);
        });
      });
  }

  termPicked(term: any) {
    const date = new Date(Date.parse(term.value));
    const dateFormatted = this.datePipe.transform(date, 'yyyy-MM-ddTHH:mm:ssZ');
    this.newAppointment.startDate = dateFormatted;
  }

  schedule() {
    this.newAppointment.patientUsername =
      this._authService.currentUserValue.userName;
    this.event.username = this._authService.currentUserValue.userName;
    this.event.step = Step.Schedule;
    this._eventService.createNewEvent(this.event).subscribe();
    this.appointmentService.scheduleAppointment(this.newAppointment).subscribe(
      (res) => {
        this.router.navigate(['/record']);
        this._snackBar.open('Appointment successfully scheduled!', 'Dismiss');
        //TODO: navigate to medical record when this component makes it to the dashboard (if that ever happens)
        //this.router.navigate(['/record']);
        window.location.reload();
      },
      (err) => {
        this._snackBar.open(
          'Appointment could not be scheduled! Please try again.',
          'Dismiss'
        );
      }
    );
  }

  appointmentBack() {
    this.event.username = this._authService.currentUserValue.userName;
    this.event.step = Step.AppointmentBack;
    this._eventService.createNewEvent(this.event).subscribe();
  }

  dateNext() {
    this.event.username = this._authService.currentUserValue.userName;
    this.event.step =Step.DateNext;
    this._eventService.createNewEvent(this.event).subscribe();
  }

  doctorBack() {
    this.event.username = this._authService.currentUserValue.userName;
    this.event.step =Step.DoctorBack;
    this._eventService.createNewEvent(this.event).subscribe();
  }

  doctorNext() {
    this.event.username = this._authService.currentUserValue.userName;
    this.event.step =Step.DoctorNext;
    this._eventService.createNewEvent(this.event).subscribe();
  }

  specializationNext() {
    this.event.username = this._authService.currentUserValue.userName;
    this.event.step =Step.SpecializationNext;
    this._eventService.createNewEvent(this.event).subscribe();
  }

  specializationBack() {
    this.event.username = this._authService.currentUserValue.userName;
    this.event.step =Step.SpecializationBack;
    console.log(this.event.step);
    this._eventService.createNewEvent(this.event).subscribe();
  }
}
