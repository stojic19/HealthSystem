import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectFloorComponent } from './select-floor.component';

describe('SelectFloorComponent', () => {
  let component: SelectFloorComponent;
  let fixture: ComponentFixture<SelectFloorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SelectFloorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectFloorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
