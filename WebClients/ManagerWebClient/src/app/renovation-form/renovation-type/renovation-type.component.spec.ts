import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RenovationTypeComponent } from './renovation-type.component';

describe('RenovationTypeComponent', () => {
  let component: RenovationTypeComponent;
  let fixture: ComponentFixture<RenovationTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RenovationTypeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RenovationTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
