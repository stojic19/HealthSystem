import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HospitalShiftsComponent } from './hospital-shifts.component';

describe('HospitalShiftsComponent', () => {
  let component: HospitalShiftsComponent;
  let fixture: ComponentFixture<HospitalShiftsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HospitalShiftsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HospitalShiftsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
