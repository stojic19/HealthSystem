import { TestBed } from '@angular/core/testing';

import { ChosenDoctorService } from './chosen-doctor.service';

describe('ChosenDoctorService', () => {
  let service: ChosenDoctorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ChosenDoctorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
