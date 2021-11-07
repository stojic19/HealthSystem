import { TestBed } from '@angular/core/testing';

import { FeedbacksManagerService } from './feedbacks-manager.service';

describe('FeedbacksManagerService', () => {
  let service: FeedbacksManagerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FeedbacksManagerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
