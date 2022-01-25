import { Component, OnInit } from '@angular/core';
import {Chart} from 'node_modules/chart.js';
import { NumberOfScheduling } from 'src/app/interfaces/number-of-scheduling';
import { StartSchedulingPerPartOfDay } from 'src/app/interfaces/start-scheduling-per-part-of-day';
import {SuccessSchedulingPerDayOfWeek } from 'src/app/interfaces/succes-scheduling-per-day-of-week';
import { EventsService } from 'src/app/services/events.service';

@Component({
  selector: 'app-event-statistic',
  templateUrl: './event-statistic.component.html',
  styleUrls: ['./event-statistic.component.css']
})
export class EventStatisticComponent implements OnInit {

  statistic! : StartSchedulingPerPartOfDay;
  statisticsPerDayOfWeek! : SuccessSchedulingPerDayOfWeek;
  statisticsPerMonths! : number[];
  numbersOfScheduling! : NumberOfScheduling[];
  constructor(private _eventService : EventsService) { 
    
  }

  ngOnInit(): void {
    this.statistic =   this._eventService.getStatisticsPerPartOfDay().subscribe({next:  (data: StartSchedulingPerPartOfDay) => {this.statistic = data; console.log(this.statistic.from0To8);
     
      const myChart = new Chart("myChart", {
        type: 'bar',
        data: {
            labels: ['00h-08h', '08h-12h', '12h-16h', '16h-20h', '20h-24h'],
            legend : {display : false},
            datasets: [{
                data: [this.statistic.from0To8, this.statistic.from8To12, this.statistic.from12To16, this.statistic.from16To20, this.statistic.from20To00],
                backgroundColor: [
                    'rgba(0, 200, 32,0.6)',
                    'rgba(255, 0, 71, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ]
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            },
            legend: {
              display: false
           }
        }
    });
  } });
  this._eventService.getStatisticsPerDayOfWeek().subscribe({next:  (data: SuccessSchedulingPerDayOfWeek) => {this.statisticsPerDayOfWeek = data;  console.log(this.statisticsPerDayOfWeek.sunday[0]);}});
  this._eventService.getStatisticsPerMonths().subscribe({next: (data: number[]) => {this.statisticsPerMonths = data;  console.log(this.statisticsPerMonths[0]);
  
    const doughnutChart = new Chart("doughnutChart", {
      type: 'doughnut',
      data: {
          labels: ['January', 'February', 'March', 'April', 'May','June', 'July', 'August', 'September', 'October','November', 'December'],
          datasets: [{
              data: [this.statisticsPerMonths[0], this.statisticsPerMonths[1], this.statisticsPerMonths[2], this.statisticsPerMonths[3],this.statisticsPerMonths[4], this.statisticsPerMonths[5],this.statisticsPerMonths[6], this.statisticsPerMonths[7],this.statisticsPerMonths[8], this.statisticsPerMonths[9],this.statisticsPerMonths[10], this.statisticsPerMonths[11]],
              backgroundColor: [
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 0, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(0, 0, 0, 0.8)',
                'rgba(255, 0, 0, 1)',
                'rgba(0, 255, 0, 1)',
                'rgba(255, 0, 71, 1)',
                'rgba(255, 255, 0, 0.75)',
                'rgba(255, 0, 255, 0.6)',
                'rgba(75, 0, 192, 1)',
                'rgba(153, 102, 0, 1)'

              ]
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

this._eventService.getNumberOfStepsSuccessScheduling().subscribe({next: (data: NumberOfScheduling[]) => {this.numbersOfScheduling = data;  
  
  const doughnutChart = new Chart("polarAreaChart", {
    type: 'polarArea',
    data: {
        labels: [this.numbersOfScheduling[0].numberOfSteps,this.numbersOfScheduling[1].numberOfSteps,this.numbersOfScheduling[2].numberOfSteps,this.numbersOfScheduling[3].numberOfSteps,this.numbersOfScheduling[4].numberOfSteps,this.numbersOfScheduling[5].numberOfSteps],
        datasets: [{
            data: [this.numbersOfScheduling[0].numberOfScheduled,this.numbersOfScheduling[1].numberOfScheduled,this.numbersOfScheduling[2].numberOfScheduled,this.numbersOfScheduling[3].numberOfScheduled,this.numbersOfScheduling[4].numberOfScheduled,this.numbersOfScheduling[5].numberOfScheduled],
            backgroundColor: [
              'rgba(54, 162, 235, 1)',
              'rgba(255, 206, 86, 1)',
              'rgba(75, 192, 0, 1)',
              'rgba(153, 102, 255, 1)',
              'rgba(0, 0, 0, 0.8)'
            ]
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
  