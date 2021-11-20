import { Component, Input, OnInit } from '@angular/core';
import { ISurvey } from 'src/app/interfaces/survey';
import { SurveyObserveService } from 'src/app/services/survey-observe.service';

@Component({
  selector: 'app-survey-section-observe',
  templateUrl: './survey-section-observe.component.html',
  styleUrls: ['./survey-section-observe.component.css']
})
export class SurveySectionObserveComponent implements OnInit {

  survey!: ISurvey;
  selectedTab!: string;
  @Input() category!: string;
  constructor(private _surveyService: SurveyObserveService) { }

  ngOnInit(): void {
    this.survey = this._surveyService.getSurvey();
  }
  

}
