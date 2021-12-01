import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { AvailableTermsRequest } from 'src/app/model/available-terms-request';
import { TimePeriod } from 'src/app/model/time-period';
import { RoomInventoriesService } from 'src/app/services/room-inventories.service';

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

  selectedTermView: TimePeriod;

  constructor() {}

  ngOnInit(): void {}

  selectTerm(term: TimePeriod) {
    this.selectedTerm.emit(term);
    this.selectedTermView = term;
    console.log(term);
  }
}
