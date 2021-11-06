import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HopsitalOverviewComponent } from './hopsital-overview.component';

describe('HopsitalOverviewComponent', () => {
  let component: HopsitalOverviewComponent;
  let fixture: ComponentFixture<HopsitalOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HopsitalOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HopsitalOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
