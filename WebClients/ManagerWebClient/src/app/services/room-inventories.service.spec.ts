import { TestBed } from '@angular/core/testing';

import { RoomInventoriesService } from './room-inventories.service';

describe('RoomInventoriesService', () => {
  let service: RoomInventoriesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RoomInventoriesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
