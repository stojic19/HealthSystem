import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FirstRoomComponent } from './first-room.component';

describe('FirstRoomComponent', () => {
  let component: FirstRoomComponent;
  let fixture: ComponentFixture<FirstRoomComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FirstRoomComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FirstRoomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
