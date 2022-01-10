import { Component, OnInit } from '@angular/core';
import { TenderingService } from 'src/app/services/tendering.service';

@Component({
  selector: 'app-tendering-statistics',
  templateUrl: './tendering-statistics.component.html',
  styleUrls: ['./tendering-statistics.component.css']
})
export class TenderingStatisticsComponent implements OnInit {

  constructor(private _TenderingService : TenderingService) { }
  ngOnInit(): void {
  }
  GetStatistics(start: HTMLInputElement, end: HTMLInputElement)
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
    this._TenderingService.getStatistics(timeRange).subscribe(data => {
      console.log(data);
    });
  }

}
