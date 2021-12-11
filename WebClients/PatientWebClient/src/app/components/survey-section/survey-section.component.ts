import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatRadioChange } from '@angular/material/radio';
import { IAnsweredQuestion } from 'src/app/interfaces/answered-question';
import { IQuestion } from 'src/app/interfaces/iquestion';
import { ISurveySection } from 'src/app/interfaces/isection';

@Component({
  selector: 'app-survey',
  templateUrl: './survey-section.component.html',
  styleUrls: ['./survey-section.component.css']
})
export class SurveySectionComponent implements OnInit {

  @Input()
  section!: ISurveySection;
  questions!: IQuestion[];
  answeredQuestions!: IAnsweredQuestion[];
  answeredQuestion!: IAnsweredQuestion;
  broj!: number;

  @Output()
  newItemEvent = new EventEmitter<IAnsweredQuestion>();

  constructor() { }

  ngOnInit(): void {
    this.questions = this.section.questions;
    this.answeredQuestions = [];

  }
  radioChange(event: MatRadioChange) {

    var value = event.value;
    value = value.split("+", 2)
    this.broj = value[0];


    this.answeredQuestion = {
      questionId: this.broj,
      rating: value[1],
      type: this.section.category

    }
    this.sendData(this.answeredQuestion);

  }

  sendData(value: IAnsweredQuestion) {
    this.newItemEvent.emit(value);
  }

}