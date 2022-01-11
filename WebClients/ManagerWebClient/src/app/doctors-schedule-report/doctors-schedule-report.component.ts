import { Component, OnInit } from '@angular/core';
import { yearsPerPage } from '@angular/material/datepicker';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import { Label } from 'ng2-charts';
import { Doctor } from '../model/doctor';
import { DoctorsReport } from '../model/doctors-report';
import { DoctorsScheduleService } from '../services/doctors-schedule.service';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatDatepicker } from '@angular/material/datepicker';

@Component({
  selector: 'app-doctors-schedule-report',
  templateUrl: './doctors-schedule-report.component.html',
  styleUrls: ['./doctors-schedule-report.component.css']
})
export class DoctorsScheduleReportComponent implements OnInit {
  reportForDoctor = new DoctorsReport();
  doctors: Doctor[];
  chosenDate: Date;
  chosenDoctor: number
  barChartData: ChartDataSets[]

  constructor(public doctorsScheduleService: DoctorsScheduleService) { }

  ngOnInit(): void {
    this.doctorsScheduleService.getAllDoctors()
      .toPromise()
      .then((res) => {
        this.doctors = res as Doctor[];
      });;

    this.setupData();
  }

  changeDoctor(value) {
    this.chosenDoctor = value;
  }

  getDoctorsReport() {
    let date = new Date(this.chosenDate)
    let year = date.getFullYear();
    let month = date.getMonth();
    let start = new Date(year, month, 1);
    let end = new Date(year, month + 1, 0);

    this.doctorsScheduleService.getReportInformation(this.chosenDoctor,
      start, end)
      .subscribe((res) => {
        this.reportForDoctor = res;
        console.log(this.reportForDoctor)
        this.setupData()
      });
  }

  setupData() {
    this.barChartData = [
      {
        data: [
          this.reportForDoctor.numOfPatients,
          this.reportForDoctor.numOfAppointments,
          this.reportForDoctor.numOfOnCallShifts,
          this.reportForDoctor.numOfAppointments + 5
        ],
        label: 'Doctors workload report',
        backgroundColor: ['#efd3d7', '#dee2ff', '#cbc0d3', '#feeafa', '#8e9aaf'],
        hoverBackgroundColor: ['#d1b2b6', '#bec3ed', '#bfa5d1'],
      },
    ];
  }

  barChartOptions: ChartOptions = {
    responsive: true,
    scales: {
      yAxes: [
        {
          ticks: {
            beginAtZero: true,
          },
        },
      ],
    },
  };

  barChartLabels: Label[] = [
    'Number of patients',
    'Number of appointments',
    'Number of on call duties',
  ];
  barChartType: ChartType = 'bar';
  barChartLegend = true;
  barChartPlugins = [];
}
