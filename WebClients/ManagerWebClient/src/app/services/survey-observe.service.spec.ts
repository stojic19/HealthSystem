import { TestBed } from '@angular/core/testing';

import { SurveyObserveService } from './survey-observe.service';

describe('SurveyObserveService', () => {
  let service: SurveyObserveService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SurveyObserveService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
