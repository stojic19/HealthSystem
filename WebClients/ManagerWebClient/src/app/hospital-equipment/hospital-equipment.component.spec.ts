import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HospitalEquipmentComponent } from './hospital-equipment.component';

describe('HospitalEquipmentComponent', () => {
  let component: HospitalEquipmentComponent;
  let fixture: ComponentFixture<HospitalEquipmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HospitalEquipmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HospitalEquipmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
