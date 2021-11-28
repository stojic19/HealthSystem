import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicationReportsComponent } from './medication-reports.component';

describe('MedicationReportsComponent', () => {
  let component: MedicationReportsComponent;
  let fixture: ComponentFixture<MedicationReportsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MedicationReportsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MedicationReportsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
