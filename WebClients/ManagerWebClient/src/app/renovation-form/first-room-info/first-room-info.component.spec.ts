import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FirstRoomInfoComponent } from './first-room-info.component';

describe('FirstRoomInfoComponent', () => {
  let component: FirstRoomInfoComponent;
  let fixture: ComponentFixture<FirstRoomInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FirstRoomInfoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FirstRoomInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
