import { TestBed } from '@angular/core/testing';

import { TenderingService } from './tendering.service';

describe('TenderingService', () => {
  let service: TenderingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TenderingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
