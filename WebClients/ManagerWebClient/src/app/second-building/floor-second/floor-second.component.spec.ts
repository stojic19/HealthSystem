import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FloorSecondComponent } from './floor-second.component';

describe('FloorSecondComponent', () => {
  let component: FloorSecondComponent;
  let fixture: ComponentFixture<FloorSecondComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FloorSecondComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FloorSecondComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
