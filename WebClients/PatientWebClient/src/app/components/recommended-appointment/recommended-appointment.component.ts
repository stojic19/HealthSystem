import { Component, OnInit } from '@angular/core';
import { IDoctor } from 'src/app/interfaces/doctor';
import { DoctorService } from 'src/app/services/DoctorService/doctor.service';
import { TimeService } from 'src/app/services/TimeService/time.service';

@Component({
  selector: 'app-recommended-appointment',
  templateUrl: './recommended-appointment.component.html',
  styleUrls: ['./recommended-appointment.component.css']
})
export class RecommendedAppointmentComponent implements OnInit {

  doctors!: IDoctor[];
  times! : string[];
  constructor(private doctorService: DoctorService,private timeService : TimeService) { 
  }

  ngOnInit(): void {
    this.doctorService.getAll().subscribe((res) => {
      this.doctors = res;
    });
    this.times=this.timeService.times;
  }

}
