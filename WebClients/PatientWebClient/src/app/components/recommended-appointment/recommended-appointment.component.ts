import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatRadioChange } from '@angular/material/radio';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { IAvailableAppointment } from 'src/app/interfaces/availableappointment';
import { IDoctor } from 'src/app/interfaces/doctor';
import { IRecommendedAppointment } from 'src/app/interfaces/irecommendedappointment';
import { AvailableAppointmentsService } from 'src/app/services/AvailableAppointmentsService/available-appointments.service';
import { DoctorService } from 'src/app/services/DoctorService/doctor.service';

@Component({
  selector: 'app-recommended-appointment',
  templateUrl: './recommended-appointment.component.html',
  styleUrls: ['./recommended-appointment.component.css'],
  providers: [DatePipe]
})
export class RecommendedAppointmentComponent implements OnInit {

  doctors!: IDoctor[];
  range = new FormGroup({
    start: new FormControl(),
    end: new FormControl(),
  });
  startDate! : string;
  endDate! : string;
  doctorId! : number;
  isDoctorPriority! : boolean;
  radioButtonSelection! : number;
  availableAppointments : IAvailableAppointment[];
  selectedAppointment! : IAvailableAppointment[];
  newAppointment : IRecommendedAppointment;
  firstFormGroup!: FormGroup;
  todayDate:Date = new Date();

  constructor(private _formBuilder: FormBuilder,
    private doctorService: DoctorService,
    private datePipe: DatePipe,
    private availableAppointmentService : AvailableAppointmentsService,
    private _snackBar: MatSnackBar,
    private router: Router) { 
      this.availableAppointments=[] as IAvailableAppointment[];
      this.availableAppointments.forEach(a=>a.doctor={}  as IDoctor);
      this.selectedAppointment=[] as IAvailableAppointment[];
      this.selectedAppointment.forEach(a=>a.doctor={}  as IDoctor);
      this.newAppointment = {} as IRecommendedAppointment;
  }

  ngOnInit(): void {
    this.range = this._formBuilder.group({
      start: ['',Validators.required],
      end: ['', Validators.required]
    });
    this.doctorService.getAll().subscribe((res) => {
      this.doctors = res;
    });
  
  }

  dateRangeChange(dateRangeStart : HTMLInputElement, dateRangeEnd: HTMLInputElement){
    this.startDate=dateRangeStart.value;
    this.endDate=dateRangeEnd.value;
  }

  getAvailableAppointments(){
    console.log(this.radioButtonSelection);
    console.log(this.doctorId);
    if(this.radioButtonSelection == 1) {
      this.isDoctorPriority = true;
    }else{
      this.isDoctorPriority = false;
    }
    this.availableAppointments=[];
    this.availableAppointmentService.getAvailableAppointments(this.doctorId, this.startDate ,this.endDate,this.isDoctorPriority)
    .subscribe((res)=>{
      console.log(res);
      res.forEach((appointment) =>{
        const template = this.datePipe.transform(
          appointment.startDate.toString(),
          'MMM dd, yyyy HH:mm'
        );
       
        this.availableAppointments.push(appointment);
      }
      )
    }
      );
  }

  radioChange(event : MatRadioChange) {
    this.radioButtonSelection = event.value;
  }

  selectionChange(event : any) {
    this.doctorId=event.value;
  }

  appointmentPicked(appointment : any){
    /*this.selectedAppointment.startDate=appointment.startDate;
    console.log(appointment);*/
  }

  onSubmit() {
    this.newAppointment={} as IRecommendedAppointment;
    this.newAppointment.doctorId = 0;
    console.log(this.selectedAppointment);
    console.log(this.newAppointment.doctorId);
    console.log(this.selectedAppointment[0].doctor.id);
    this.newAppointment.doctorId=this.selectedAppointment[0].doctor.id;
    this.newAppointment.startDate=this.selectedAppointment[0].startDate;
    this.createAppointment();

  }

  createAppointment() {
    this.availableAppointmentService.createNewAppointment(this.newAppointment).subscribe(
      (res) => {
        this.router.navigate(['/record']);
        this._snackBar.open(
          'Scheduling was successful.',
          'Dismiss'
        );
      },
      (err) => {
        let parts = err.error.split(':');
        let mess = parts[parts.length - 1];
        let description = mess.substring(1, mess.length - 4);
        this._snackBar.open(description, 'Dismiss');
      }
    
    );
  }
}
