import { TestBed } from '@angular/core/testing';

import { DoctorShiftService } from './doctor-shift.service';

describe('DoctorShiftService', () => {
  let service: DoctorShiftService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DoctorShiftService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
