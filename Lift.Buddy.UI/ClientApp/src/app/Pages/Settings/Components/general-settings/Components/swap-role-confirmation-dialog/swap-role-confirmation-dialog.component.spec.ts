import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SwapRoleConfirmationDialogComponent } from './swap-role-confirmation-dialog.component';

describe('SwapRoleConfirmationDialogComponent', () => {
  let component: SwapRoleConfirmationDialogComponent;
  let fixture: ComponentFixture<SwapRoleConfirmationDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SwapRoleConfirmationDialogComponent]
    });
    fixture = TestBed.createComponent(SwapRoleConfirmationDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
