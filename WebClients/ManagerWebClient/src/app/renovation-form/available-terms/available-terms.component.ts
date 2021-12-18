import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { TimePeriod } from 'src/app/model/time-period';

@Component({
  selector: 'app-available-terms',
  templateUrl: './available-terms.component.html',
  styleUrls: ['./available-terms.component.css']
})
export class AvailableTermsComponent implements OnInit {

  @Input()
  availableTerms: TimePeriod[];
  @Output()
  selectedTerm = new EventEmitter<TimePeriod>();

  selectedTermView: TimePeriod;

  constructor() { }

  ngOnInit(): void { }

  selectTerm(term: TimePeriod) {
    this.selectedTerm.emit(term);
    this.selectedTermView = term;
    console.log(term);
  }

}
