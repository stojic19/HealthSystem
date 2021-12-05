import { IAnsweredSurvey } from './../../interfaces/answered-survey-interface';
import { SurveyService } from './../../services/SurveyService/survey.service';
import { IAnsweredQuestion } from 'src/app/interfaces/answered-question-interface';
import { Component, Input, OnInit } from '@angular/core';
import { ISurvey } from 'src/app/interfaces/survey/isurvey';
import { IScheduledEvent } from 'src/app/interfaces/scheduled-event-interface';
import { MatSnackBar } from '@angular/material/snack-bar';



@Component({
  selector: 'app-survey-page',
  templateUrl: './survey-page.component.html',
  styleUrls: ['./survey-page.component.css']
})
export class SurveyPageComponent implements OnInit {

  survey!: ISurvey;
  numberOfSurveys!: number;
  scheduledEvent!: IScheduledEvent;
  answeredQuestions!: IAnsweredQuestion[];
  totalQuestions!: number;
  answeredSurvey!: IAnsweredSurvey;

  constructor(private snackBar: MatSnackBar, private surveyService: SurveyService) {
    this.answeredQuestions = [];

  }

  addQuestion(answeredQuestion: IAnsweredQuestion) {
    var question = this.answeredQuestions.find(q => q.questionId == answeredQuestion.questionId);
    if (question == null) {
      this.answeredQuestions.push(Object.assign({}, answeredQuestion))
    }
    else {
      question.rating = answeredQuestion.rating;
    }

    console.log(this.answeredQuestions);
  }
  saveSurvey() {
    if (this.answeredQuestions.length == this.totalQuestions) {

      this.answeredSurvey = {
        surveyId: this.survey.surveyId,
        questions: this.answeredQuestions,
        scheduledEventId: this.scheduledEvent.id
      }
      this.surveyService.answerSurvey(this.answeredSurvey).subscribe();
      this.snackBar.open("Thank you for answering our Survey . ", '', {
        duration: 3000,
        verticalPosition: 'bottom'

      });
    } else {
      this.snackBar.open("You need to answer all questions. ", '', {
        duration: 3000,
        verticalPosition: 'bottom'

      });
    }
  }
  ngOnInit(): void {

    this.scheduledEvent = {
      id: 2,
      isDone: true,
      startDate: new Date('October 17, 2021 15:30:00'),
      endDate: new Date('October 17, 2021 16:24:00')

    }

    this.surveyService.getSurvey().subscribe((res: ISurvey) => {
      console.log(res);
      this.survey = res;
      this.totalQuestions = this.survey.hospitalSection.questions.length + this.survey.doctorSection.questions.length + this.survey.medicalStaffSection.questions.length;
    })



  }

}
