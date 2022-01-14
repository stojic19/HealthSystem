import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TenderingStatisticsComponent } from './tendering-statistics.component';

describe('TenderingStatisticsComponent', () => {
  let component: TenderingStatisticsComponent;
  let fixture: ComponentFixture<TenderingStatisticsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TenderingStatisticsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TenderingStatisticsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
