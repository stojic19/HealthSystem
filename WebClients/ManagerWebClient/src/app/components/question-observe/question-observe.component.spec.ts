import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionObserveComponent } from './question-observe.component';

describe('QuestionObserveComponent', () => {
  let component: QuestionObserveComponent;
  let fixture: ComponentFixture<QuestionObserveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuestionObserveComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(QuestionObserveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
