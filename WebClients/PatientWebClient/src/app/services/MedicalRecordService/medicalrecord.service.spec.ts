import { TestBed } from '@angular/core/testing';

import { MedicalrecordService } from './medicalrecord.service';

describe('MedicalrecordService', () => {
  let service: MedicalrecordService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MedicalrecordService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
