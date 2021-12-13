import { TestBed } from '@angular/core/testing';

import { RoomRenovationService } from './room-renovation.service';

describe('RoomRenovationService', () => {
  let service: RoomRenovationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RoomRenovationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
