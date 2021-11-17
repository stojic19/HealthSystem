import { IQuestion } from './../../interfaces/survey/iquestion';
import { ISurveySection } from './../../interfaces/survey/isection';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatRadioChange } from '@angular/material/radio';



@Component({
  selector: 'app-survey',
  templateUrl: './survey.component.html',
  styleUrls: ['./survey.component.css']
})
export class SurveyComponent implements OnInit {

  @Input()
  section!: ISurveySection;
  questions!: IQuestion[];

  constructor() { }

  ngOnInit(): void {
    this.questions = this.section.questions;

  }
  radioChange(event: MatRadioChange) {
    console.log(event.value);


  }



}
