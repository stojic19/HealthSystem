import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OnCallShiftsComponent } from './on-call-shifts.component';

describe('OnCallShiftsComponent', () => {
  let component: OnCallShiftsComponent;
  let fixture: ComponentFixture<OnCallShiftsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OnCallShiftsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OnCallShiftsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
