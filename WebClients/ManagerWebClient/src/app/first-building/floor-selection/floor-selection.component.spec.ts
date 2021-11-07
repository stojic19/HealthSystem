import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FloorSelectionComponent } from './floor-selection.component';

describe('FloorSelectionComponent', () => {
  let component: FloorSelectionComponent;
  let fixture: ComponentFixture<FloorSelectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FloorSelectionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FloorSelectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
