import { TestBed } from '@angular/core/testing';

import { RoomPositionService } from './room-position.service';

describe('RoomPositionService', () => {
  let service: RoomPositionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RoomPositionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
