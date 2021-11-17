import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DestinationRoomComponent } from './destination-room.component';

describe('DestinationRoomComponent', () => {
  let component: DestinationRoomComponent;
  let fixture: ComponentFixture<DestinationRoomComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DestinationRoomComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DestinationRoomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
