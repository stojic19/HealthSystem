import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InitialRoomComponent } from './initial-room.component';

describe('InitialRoomComponent', () => {
  let component: InitialRoomComponent;
  let fixture: ComponentFixture<InitialRoomComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InitialRoomComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InitialRoomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
