import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecommendedAppointmentComponent } from './recommended-appointment.component';

describe('RecommendedAppointmentComponent', () => {
  let component: RecommendedAppointmentComponent;
  let fixture: ComponentFixture<RecommendedAppointmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RecommendedAppointmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RecommendedAppointmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
