import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmEnrollmentComponent } from './confirm-enrollment.component';

describe('ConfirmEnrollmentComponent', () => {
  let component: ConfirmEnrollmentComponent;
  let fixture: ComponentFixture<ConfirmEnrollmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfirmEnrollmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfirmEnrollmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
