import { TestBed } from '@angular/core/testing';

import { AvailableAppointmentsService } from './available-appointments.service';

describe('AvailableAppointmentsService', () => {
  let service: AvailableAppointmentsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AvailableAppointmentsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
