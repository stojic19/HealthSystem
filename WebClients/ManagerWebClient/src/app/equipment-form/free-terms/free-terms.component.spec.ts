import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FreeTermsComponent } from './free-terms.component';

describe('FreeTermsComponent', () => {
  let component: FreeTermsComponent;
  let fixture: ComponentFixture<FreeTermsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FreeTermsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FreeTermsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
