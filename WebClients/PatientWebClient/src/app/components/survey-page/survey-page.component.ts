import { IAnsweredQuestion } from 'src/app/interfaces/answered-question-interface';
import { Component, Input, OnInit } from '@angular/core';
import { ISurvey } from 'src/app/interfaces/survey/isurvey';
import { IScheduledEvent } from 'src/app/interfaces/scheduled-event-interface';
import { SurveyCategory } from 'src/app/interfaces/survey/isection';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-survey-page',
  templateUrl: './survey-page.component.html',
  styleUrls: ['./survey-page.component.css']
})
export class SurveyPageComponent implements OnInit {

  survey!: ISurvey;
  numberOfSurveys!: number;
  termin!: IScheduledEvent;
  answeredQuestions!: IAnsweredQuestion[];
  totalQuestions!: number;

  constructor(private snackBar: MatSnackBar) {
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
      //TODO:Post Request
    } else {
      this.snackBar.open("You need to answer all questions. ", '', {
        duration: 3000,
        verticalPosition: 'bottom'

      });
    }
  }
  ngOnInit(): void {

    this.termin = {
      id: 1,
      isDone: true,
      startTime: new Date('October 17, 2021 15:30:00'),
      endTime: new Date('October 17, 2021 16:24:00')

    }

    //#region 
    this.survey = {
      id: 0,
      doctorSection: {
        name: 'Doctor Survey',
        questions: [{
          id: 10,
          text: 'How long did you have to wait until the doctor attends to you?',

        },
        {
          id: 11,
          text: ' Were you satisfied with the doctor you were allocated with?',

        },
        {
          id: 12,
          text: ' How happy are you with the doctor’s treatment?',

        },
        {
          id: 13,
          text: 'How would you rate the professionalism of doctor?',

        },
        {
          id: 14,
          text: ' What is your overall satisfaction with doctor?',

        }],
        category: SurveyCategory.DoctorSurvey
      },
      medicalStaffSection: {
        name: 'Medical Staff Survey',
        questions: [{
          id: 0,
          text: 'Were our staff empathetic to your needs?',

        },
        {
          id: 1,
          text: 'How would you rate the professionalism of our staff?',

        },
        {
          id: 2,
          text: 'Were the staff quick to respond to your medical care request?',

        },
        {
          id: 3,
          text: 'How would you rate courtesy of our staff?',

        },
        {
          id: 4,
          text: ' What is your overall satisfaction with staff?',

        }],
        category: SurveyCategory.StaffSurvey
      },
      hospitalSection: {
        name: 'Hospital Survey',
        questions: [{
          id: 5,
          text: ' How did you find the experience of booking appointments?',

        },
        {
          id: 6,
          text: ' How easy is it to navigate our application?',

        },
        {
          id: 7,
          text: ' Were we able to answer all your questions',

        },
        {
          id: 8,
          text: ' How likely are you to recommend us to your friends and family?',

        },
        {
          id: 9,
          text: ' What is your overall satisfaction with application?',

        }],
        category: SurveyCategory.HospitalSurvey
      }


    }
    this.totalQuestions = this.survey.hospitalSection.questions.length + this.survey.doctorSection.questions.length + this.survey.medicalStaffSection.questions.length;

  }
  //#endregion

}
