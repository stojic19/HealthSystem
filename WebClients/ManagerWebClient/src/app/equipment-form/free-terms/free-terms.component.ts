import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { TimePeriod } from 'src/app/model/time-period';

@Component({
  selector: 'app-free-terms',
  templateUrl: './free-terms.component.html',
  styleUrls: ['./free-terms.component.css'],
})
export class FreeTermsComponent implements OnInit {
  @Input()
  freeTerms: TimePeriod[];
  @Output()
  selectedTerm = new EventEmitter<TimePeriod>();

  constructor() {}

  ngOnInit(): void {}

  selectTerm(term: TimePeriod) {
    this.selectedTerm.emit(term);
  }
}
