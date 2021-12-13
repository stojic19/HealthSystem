import { Component, OnInit } from '@angular/core';
import { MedicationReportService } from 'src/app/services/medication-report.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-medication-reports',
  templateUrl: './medication-reports.component.html',
  styleUrls: ['./medication-reports.component.css']
})
export class MedicationReportsComponent implements OnInit {

  constructor(private _MedicationReportService : MedicationReportService) { }
  report:any;
  ngOnInit(): void {
  }
  GetReport(start: HTMLInputElement, end: HTMLInputElement)
  {
    if(start.value == "" || end.value == "")
    {
      alert("Please enter date range");
      return;
    }
    
    var startDate = new Date(start.value);
    var userTimezoneOffset = startDate.getTimezoneOffset() * 60000;
    startDate = new Date(startDate.getTime() - userTimezoneOffset);
    var endDate = new Date(end.value);
    var userTimezoneOffset = startDate.getTimezoneOffset() * 60000;
    endDate = new Date(endDate.getTime() - userTimezoneOffset);
    var timeRange = {startTime: startDate, endTime: endDate};
    this._MedicationReportService.getReport(timeRange).subscribe(data => {
      this.report = data
      console.log(this.report);
    });
  }
  SendReport()
  {    
    let report = 
    {
      startDate: this.report.startDate,
      endDate: this.report.endDate,
      createdDate: this.report.createdDate,
      medicationExpenditureDTO: this.report.medicationExpenditureDTO
    }
    this._MedicationReportService.sendReport(report).subscribe(data => {
      this.report = null;
      console.log(data);
      alert(data);
    })
  }
}
