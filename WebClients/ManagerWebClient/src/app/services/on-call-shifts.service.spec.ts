import { TestBed } from '@angular/core/testing';

import { OnCallShiftsService } from './on-call-shifts.service';

describe('OnCallShiftsService', () => {
  let service: OnCallShiftsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OnCallShiftsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
