import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisplayRoomInfoComponent } from './display-room-info.component';

describe('DisplayRoomInfoComponent', () => {
  let component: DisplayRoomInfoComponent;
  let fixture: ComponentFixture<DisplayRoomInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DisplayRoomInfoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DisplayRoomInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
