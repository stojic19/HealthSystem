import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FloorFirstComponent } from './floor-first.component';

describe('FloorFirstComponent', () => {
  let component: FloorFirstComponent;
  let fixture: ComponentFixture<FloorFirstComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FloorFirstComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FloorFirstComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
