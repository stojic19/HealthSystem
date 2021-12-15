import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SecondRoomInfoComponent } from './second-room-info.component';

describe('SecondRoomInfoComponent', () => {
  let component: SecondRoomInfoComponent;
  let fixture: ComponentFixture<SecondRoomInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SecondRoomInfoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SecondRoomInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
