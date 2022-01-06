import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
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
  errorText: string;
  constructor(private _surveyService: SurveyObserveService,private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.survey = this._surveyService.getSurvey().subscribe({
      next:  (data: ISurveyStatistic) => {this.survey = data;
        try {
          this.selectedSection = this.survey.categoriesStatistic[0]!;
        this.selectedSection.questionsStatistic = this.survey.categoriesStatistic[0].questionsStatistic!;
        } catch (error) {
        }
        
        },
        error: (err: any) => {
          console.log(err);
          this._snackBar.open(err.error, "Dismiss");
          this.errorText = err.error;
          
        },
  })
  }
  tabSelected(event: MatTabChangeEvent){
    this.selectedSection = this.survey.categoriesStatistic[event.index];

}

}
