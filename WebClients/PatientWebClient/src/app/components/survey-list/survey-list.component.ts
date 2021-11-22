import { Router } from '@angular/router';
import { IScheduledEvent } from 'src/app/interfaces/scheduled-event-interface';
import { ISurvey } from 'src/app/interfaces/survey/isurvey';
import { Component, OnInit } from '@angular/core';
import { MatTable } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule } from '@angular/router';



@Component({
  selector: 'app-survey-list',
  templateUrl: './survey-list.component.html',
  styleUrls: ['./survey-list.component.css']
})

export class SurveyListComponent implements OnInit {

  columnsToDisplay: string[] = ['No.', 'Date', 'Time', 'Button'];
  allEvents!: IScheduledEvent[];


  constructor(private Router: Router) { }

  openSurveyPage(id: number) {
    // <tr mat-row *matRowDef="let element; columns: columnsToDisplay" routerLink="/surveys/{{element.id}}">
    this.Router.navigateByUrl("/surveys/" + id);
  }

  ngOnInit(): void {
    //#region 
    this.allEvents = [{
      id: 0,
      isDone: true,
      startTime: new Date('October 17, 2021 15:30:00'),
      endTime: new Date('October 17, 2021 16:24:00')
    },
    {
      id: 1,
      isDone: true,
      startTime: new Date('September 21, 2021 10:30:00'),
      endTime: new Date('September 21, 2021 11:24:00')

    },
    {
      id: 2,
      isDone: true,
      startTime: new Date('June 10, 2021 14:30:00'),
      endTime: new Date('June 10, 2021 15:24:00')

    }

    ]

  }
  //#endregion


}
