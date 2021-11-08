import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SecondBuildingComponent } from './second-building.component';

describe('SecondBuildingComponent', () => {
  let component: SecondBuildingComponent;
  let fixture: ComponentFixture<SecondBuildingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SecondBuildingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SecondBuildingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
