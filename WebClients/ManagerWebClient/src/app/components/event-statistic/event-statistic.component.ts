import { Component, OnInit } from '@angular/core';
import {Chart} from 'node_modules/chart.js';
import { StartSchedulingPerPartOfDay } from 'src/app/interfaces/start-scheduling-per-part-of-day';
import { EventsService } from 'src/app/services/events.service';

@Component({
  selector: 'app-event-statistic',
  templateUrl: './event-statistic.component.html',
  styleUrls: ['./event-statistic.component.css']
})
export class EventStatisticComponent implements OnInit {

  statistic! : StartSchedulingPerPartOfDay;
  constructor(private _eventService : EventsService) { 
  }

  ngOnInit(): void {
    this.statistic =   this._eventService.getStatisticsPerPartOfDay().subscribe({next:  (data: StartSchedulingPerPartOfDay) => {this.statistic = data; console.log(this.statistic.from0To8);
     
      const myChart = new Chart("myChart", {
        type: 'bar',
        data: {
            labels: ['00h-08h', '08h-12h', '12h-16h', '16h-20h', '20h-24h'],
            datasets: [{
                label:'Number of started scheduling per part of day',
                data: [this.statistic.from0To8, this.statistic.from8To12, this.statistic.from12To16, this.statistic.from16To20, this.statistic.from20To00],
                backgroundColor: [
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
  } });
  
}}
  