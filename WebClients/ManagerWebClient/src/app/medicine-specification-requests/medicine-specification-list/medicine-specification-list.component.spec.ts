import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicineSpecificationListComponent } from './medicine-specification-list.component';

describe('MedicineSpecificationListComponent', () => {
  let component: MedicineSpecificationListComponent;
  let fixture: ComponentFixture<MedicineSpecificationListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MedicineSpecificationListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MedicineSpecificationListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
