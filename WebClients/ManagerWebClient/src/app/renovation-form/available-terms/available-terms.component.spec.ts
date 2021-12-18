import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AvailableTermsComponent } from './available-terms.component';

describe('AvailableTermsComponent', () => {
  let component: AvailableTermsComponent;
  let fixture: ComponentFixture<AvailableTermsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AvailableTermsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AvailableTermsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
