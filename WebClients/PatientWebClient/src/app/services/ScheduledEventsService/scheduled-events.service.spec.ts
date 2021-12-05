import { TestBed } from '@angular/core/testing';

import { ScheduledEventsService } from './scheduled-events.service';

describe('ScheduledEventsService', () => {
  let service: ScheduledEventsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ScheduledEventsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
