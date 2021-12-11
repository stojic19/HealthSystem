import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SurroundingRoomComponent } from './surrounding-room.component';

describe('SurroundingRoomComponent', () => {
  let component: SurroundingRoomComponent;
  let fixture: ComponentFixture<SurroundingRoomComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SurroundingRoomComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SurroundingRoomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
