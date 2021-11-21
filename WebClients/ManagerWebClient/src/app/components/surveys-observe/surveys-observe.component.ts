import { Component, OnInit } from '@angular/core';
import { SurveyObserveService } from 'src/app/services/survey-observe.service';
import { ISurvey } from '../../interfaces/survey';
import { MatTabChangeEvent } from '@angular/material/tabs';
@Component({
  selector: 'app-surveys-observe',
  templateUrl: './surveys-observe.component.html',
  styleUrls: ['./surveys-observe.component.css'],
  providers: [SurveyObserveService]
})
export class SurveysObserveComponent implements OnInit {

  survey!: ISurvey;
  categoryTab: string = 'Hospital';
  constructor(private _surveyService: SurveyObserveService) { }

  ngOnInit(): void {
    this.survey = this._surveyService.getSurvey();
  }
  tabSelected(event: MatTabChangeEvent){
      this.categoryTab = event.tab.textLabel;
      console.log(this.categoryTab);
  }

}
