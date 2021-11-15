import { IQuestion } from './../../interfaces/survey/iquestion';
import { ISurveySection } from './../../interfaces/survey/isection';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-survey',
  templateUrl: './survey.component.html',
  styleUrls: ['./survey.component.css']
})
export class SurveyComponent implements OnInit {

  @Input()
  section!: ISurveySection;
  questions!: IQuestion[];

  userForm = new FormGroup({
    0: new FormControl('', Validators.required),
    1: new FormControl('', Validators.required),
    2: new FormControl('', Validators.required),
    3: new FormControl('', Validators.requiredTrue),
    4: new FormControl('', Validators.requiredTrue)
  });
  constructor() { }

  ngOnInit(): void {
    this.questions = this.section.questions;

  }
  onFormSubmit(): void {
    console.log("ispisi nesto");


  }

}
