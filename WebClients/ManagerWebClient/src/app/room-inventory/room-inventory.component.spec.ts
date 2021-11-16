import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoomInventoryComponent } from './room-inventory.component';

describe('RoomInventoryComponent', () => {
  let component: RoomInventoryComponent;
  let fixture: ComponentFixture<RoomInventoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RoomInventoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RoomInventoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
