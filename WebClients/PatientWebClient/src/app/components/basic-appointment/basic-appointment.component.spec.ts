import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BasicAppointmentComponent } from './basic-appointment.component';

describe('BasicAppointmentComponent', () => {
  let component: BasicAppointmentComponent;
  let fixture: ComponentFixture<BasicAppointmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BasicAppointmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BasicAppointmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
