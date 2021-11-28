import { TestBed } from '@angular/core/testing';

import { MedicineSpecificationRequestsService } from './medicine-specification-requests.service';

describe('MedicineSpecificationRequestsService', () => {
  let service: MedicineSpecificationRequestsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MedicineSpecificationRequestsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
