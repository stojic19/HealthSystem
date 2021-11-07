import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FirstBuildingComponent } from './first-building.component';

describe('FirstBuildingComponent', () => {
  let component: FirstBuildingComponent;
  let fixture: ComponentFixture<FirstBuildingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FirstBuildingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FirstBuildingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
