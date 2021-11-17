import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterPharamcyComponent } from './register-pharamcy.component';

describe('RegisterPharamcyComponent', () => {
  let component: RegisterPharamcyComponent;
  let fixture: ComponentFixture<RegisterPharamcyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegisterPharamcyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterPharamcyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
