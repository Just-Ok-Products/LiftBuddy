/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { CreateUpdateWorkoutplanPageComponent } from './create-update-workoutplan-page.component';

describe('CreateUpdateWorkoutplanPageComponent', () => {
  let component: CreateUpdateWorkoutplanPageComponent;
  let fixture: ComponentFixture<CreateUpdateWorkoutplanPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateUpdateWorkoutplanPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateUpdateWorkoutplanPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
