import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorsScheduleReportComponent } from './doctors-schedule-report.component';

describe('DoctorsScheduleReportComponent', () => {
  let component: DoctorsScheduleReportComponent;
  let fixture: ComponentFixture<DoctorsScheduleReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DoctorsScheduleReportComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DoctorsScheduleReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
