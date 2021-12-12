import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RenovationFormComponent } from './renovation-form.component';

describe('RenovationFormComponent', () => {
  let component: RenovationFormComponent;
  let fixture: ComponentFixture<RenovationFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RenovationFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RenovationFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
