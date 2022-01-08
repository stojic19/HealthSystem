import { Component, OnInit } from '@angular/core';
import { yearsPerPage } from '@angular/material/datepicker';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import { Label } from 'ng2-charts';
import { DoctorsReport } from '../model/doctors-report';

@Component({
  selector: 'app-doctors-schedule-report',
  templateUrl: './doctors-schedule-report.component.html',
  styleUrls: ['./doctors-schedule-report.component.css']
})
export class DoctorsScheduleReportComponent implements OnInit {
  reportForDoctor: DoctorsReport = {
    id: 1,
    numOfPatients: 10,
    numOfAppointments: 10,
    numOfOnCallDuties: 3,
  };

  months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December']
  years = [] as any;

  currentYear = new Date().getFullYear()
  min = this.currentYear - 10

  constructor() { }

  ngOnInit(): void {
    for (let year = this.min; year <= this.currentYear; year++) {
      this.years.push(year);
    }
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

  barChartData: ChartDataSets[] = [
    {
      data: [
        this.reportForDoctor.numOfPatients,
        this.reportForDoctor.numOfAppointments,
        this.reportForDoctor.numOfOnCallDuties,
        this.reportForDoctor.numOfAppointments + 5,
      ],
      label: 'Doctors workload report',
      backgroundColor: ['#efd3d7', '#dee2ff', '#cbc0d3', '#feeafa', '#8e9aaf'],
      hoverBackgroundColor: ['#d1b2b6', '#bec3ed', '#bfa5d1'],
    },
  ];
}
