import { SurveyService } from './../../services/SurveyService/survey.service';
import { IAnsweredQuestion } from 'src/app/interfaces/answered-question';
import { Component, Input, OnInit } from '@angular/core';
import { ISurvey } from 'src/app/interfaces/isurvey';
import { IScheduledEvent } from 'src/app/interfaces/scheduled-event';
import { MatSnackBar } from '@angular/material/snack-bar';
import { IAnsweredSurvey } from 'src/app/interfaces/answered-survey';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { IAppointment } from 'src/app/interfaces/appointment';
import { AppointmentService } from 'src/app/services/AppointmentService/appointment.service';



@Component({
  selector: 'app-survey-page',
  templateUrl: './survey-page.component.html',
  styleUrls: ['./survey-page.component.css']
})
export class SurveyPageComponent implements OnInit {

  survey!: ISurvey;
  numberOfSurveys!: number;
  scheduledEvent!: IAppointment;
  answeredQuestions!: IAnsweredQuestion[];
  totalQuestions!: number;
  answeredSurvey!: IAnsweredSurvey;
  appointmentId: any;

  constructor(private snackBar: MatSnackBar,private appointmentService:AppointmentService, private surveyService: SurveyService, private route: ActivatedRoute,
    private _router: Router) {
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
      
      this._router.navigate(['/record']).then(() => {
        window.location.reload();
      });
      
    } else {
      this.snackBar.open("You need to answer all questions. ", '', {
        duration: 3000,
        verticalPosition: 'bottom'

      });
    }
  }
  ngOnInit(): void {

    this.appointmentId = this.route.snapshot.paramMap.get('appointmentId');
    console.log(this.appointmentId);
      
    this.appointmentService.getAppointment(this.appointmentId).subscribe((res:IAppointment)=>{
      this.scheduledEvent = res;
    })

    this.surveyService.getSurvey().subscribe((res: ISurvey) => {
      this.survey = res;
      this.totalQuestions = this.survey.hospitalSection.questions.length + this.survey.doctorSection.questions.length + this.survey.medicalStaffSection.questions.length;
    })



  }

}