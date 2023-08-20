/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { YourWorkoutsPageComponent } from './your-workouts-page.component';

describe('YourWorkoutsPageComponent', () => {
  let component: YourWorkoutsPageComponent;
  let fixture: ComponentFixture<YourWorkoutsPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ YourWorkoutsPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(YourWorkoutsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
