import { ISurveySection } from './../../interfaces/survey/isection';
import { Component, OnInit } from '@angular/core';
import { ISurvey } from 'src/app/interfaces/survey/isurvey';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';

@Component({
  selector: 'app-survey-page',
  templateUrl: './survey-page.component.html',
  styleUrls: ['./survey-page.component.css']
})
export class SurveyPageComponent implements OnInit {

  survey!: ISurvey;

  constructor() { }

  ngOnInit(): void {


    this.survey = {
      id: 0,
      doctorSection: {
        name: 'Doctor Survey',
        questions: [{
          id: 10,
          text: 'How long did you have to wait until the doctor attends to you?',
          rating: 0
        },
        {
          id: 11,
          text: ' Were you satisfied with the doctor you were allocated with?',
          rating: 0
        },
        {
          id: 12,
          text: ' How happy are you with the doctor’s treatment?',
          rating: 0
        },
        {
          id: 13,
          text: 'How would you rate the professionalism of doctor?',
          rating: 0
        },
        {
          id: 14,
          text: ' What is your overall satisfaction with doctor?',
          rating: 0
        }


        ]
      },
      medicalStaffSection: {
        name: 'Medical Staff Survey',
        questions: [{
          id: 0,
          text: 'Were our staff empathetic to your needs?',
          rating: 0
        },
        {
          id: 1,
          text: 'How would you rate the professionalism of our staff?',
          rating: 0
        },
        {
          id: 2,
          text: 'Were the staff quick to respond to your medical care request?',
          rating: 0
        },
        {
          id: 3,
          text: 'How would you rate courtesy of our staff?',
          rating: 0

        },
        {
          id: 4,
          text: ' What is your overall satisfaction with staff?',
          rating: 0
        }


        ]
      },
      hospitalSection: {
        name: 'Hospital Survey',
        questions: [{
          id: 5,
          text: ' How did you find the experience of booking appointments?',
          rating: 0
        },
        {
          id: 6,
          text: ' How easy is it to navigate our application?',
          rating: 0
        },
        {
          id: 7,
          text: ' Were we able to answer all your questions',
          rating: 0
        },
        {
          id: 8,
          text: ' How likely are you to recommend us to your friends and family?',
          rating: 0
        },
        {
          id: 9,
          text: ' What is your overall satisfaction with application?',
          rating: 0
        }

        ]
      }
    }

  }

}
