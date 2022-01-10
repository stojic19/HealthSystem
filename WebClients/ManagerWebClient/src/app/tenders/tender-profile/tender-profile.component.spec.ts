import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TenderProfileComponent } from './tender-profile.component';

describe('TenderProfileComponent', () => {
  let component: TenderProfileComponent;
  let fixture: ComponentFixture<TenderProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TenderProfileComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TenderProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
