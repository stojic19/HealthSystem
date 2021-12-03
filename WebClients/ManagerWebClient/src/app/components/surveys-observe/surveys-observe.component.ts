import { Component, OnInit } from '@angular/core';
import { MatTabChangeEvent } from '@angular/material/tabs';
import { Subscription } from 'rxjs';
import { ISurveySectionStatistic, SurveySectionStatistic } from 'src/app/interfaces/survey-section-statistic';
import { ISurveyStatistic } from 'src/app/interfaces/survey-statistic';
import { SurveyObserveService } from 'src/app/services/survey-observe.service';

@Component({
  selector: 'app-surveys-observe',
  templateUrl: './surveys-observe.component.html',
  styleUrls: ['./surveys-observe.component.css'],
  providers: [SurveyObserveService]
})
export class SurveysObserveComponent implements OnInit {

  survey!: ISurveyStatistic;
  selectedSection: ISurveySectionStatistic = new SurveySectionStatistic();
  sub! : Subscription;
  constructor(private _surveyService: SurveyObserveService) { }

  ngOnInit(): void {
    this.survey = this._surveyService.getSurvey().subscribe({
      next:  (data: ISurveyStatistic) => {this.survey = data;
        this.selectedSection = this.survey.categoriesStatistic[0];
        this.selectedSection.questionsStatistic = this.survey.categoriesStatistic[0].questionsStatistic;
        }
  })
  }
  tabSelected(event: MatTabChangeEvent){
    this.selectedSection = this.survey.categoriesStatistic[event.index];

}

}
