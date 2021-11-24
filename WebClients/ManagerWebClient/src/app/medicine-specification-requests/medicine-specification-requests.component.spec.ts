import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicineSpecificationRequestsComponent } from './medicine-specification-requests.component';

describe('MedicineSpecificationRequestsComponent', () => {
  let component: MedicineSpecificationRequestsComponent;
  let fixture: ComponentFixture<MedicineSpecificationRequestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MedicineSpecificationRequestsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MedicineSpecificationRequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
