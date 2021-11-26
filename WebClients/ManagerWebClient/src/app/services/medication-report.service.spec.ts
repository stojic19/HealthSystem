import { TestBed } from '@angular/core/testing';

import { MedicationReportService } from './medication-report.service';

describe('MedicationReportService', () => {
  let service: MedicationReportService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MedicationReportService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
