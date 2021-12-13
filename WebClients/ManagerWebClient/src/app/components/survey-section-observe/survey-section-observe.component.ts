import { Component, Input, OnInit } from '@angular/core';
import { ISurveySectionStatistic } from 'src/app/interfaces/survey-section-statistic';
import { ISurveyStatistic } from 'src/app/interfaces/survey-statistic';
import { SurveyObserveService } from 'src/app/services/survey-observe.service';

@Component({
  selector: 'app-survey-section-observe',
  templateUrl: './survey-section-observe.component.html',
  styleUrls: ['./survey-section-observe.component.css']
})
export class SurveySectionObserveComponent implements OnInit {

  survey!: ISurveyStatistic;
  selectedTab!: string;
  @Input() section: ISurveySectionStatistic;
  constructor(private _surveyService: SurveyObserveService) {  }

  ngOnInit(): void {
  }

}
