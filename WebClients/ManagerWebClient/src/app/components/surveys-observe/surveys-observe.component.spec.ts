import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SurveysObserveComponent } from './surveys-observe.component';

describe('SurveysObserveComponent', () => {
  let component: SurveysObserveComponent;
  let fixture: ComponentFixture<SurveysObserveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SurveysObserveComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SurveysObserveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
