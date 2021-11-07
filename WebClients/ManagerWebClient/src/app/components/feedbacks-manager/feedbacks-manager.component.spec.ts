import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FeedbacksManagerComponent } from './feedbacks-manager.component';

describe('FeedbacksManagerComponent', () => {
  let component: FeedbacksManagerComponent;
  let fixture: ComponentFixture<FeedbacksManagerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FeedbacksManagerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FeedbacksManagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
