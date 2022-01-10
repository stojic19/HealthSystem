import { TestBed } from '@angular/core/testing';

import { DoctorsScheduleService } from './doctors-schedule.service';

describe('DoctorsScheduleService', () => {
  let service: DoctorsScheduleService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DoctorsScheduleService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
