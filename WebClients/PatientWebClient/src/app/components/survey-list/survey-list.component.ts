import { ScheduledEventsService } from './../../services/ScheduledEventsService/scheduled-events.service';
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


  constructor(private Router: Router, private eventsService: ScheduledEventsService) { }

  openSurveyPage(id: number) {
    // <tr mat-row *matRowDef="let element; columns: columnsToDisplay" routerLink="/surveys/{{element.id}}">
    this.Router.navigateByUrl("/surveys/" + id);
  }

  ngOnInit(): void {
    //TODO: User service to return user Id so I can query 
    this.eventsService.getEventsForPatient().subscribe((res) => {
      console.log(res);

      this.allEvents = res;
    })

  }


}
