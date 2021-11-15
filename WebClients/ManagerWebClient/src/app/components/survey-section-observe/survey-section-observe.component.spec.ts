import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SurveySectionObserveComponent } from './survey-section-observe.component';

describe('SurveySectionObserveComponent', () => {
  let component: SurveySectionObserveComponent;
  let fixture: ComponentFixture<SurveySectionObserveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SurveySectionObserveComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SurveySectionObserveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
