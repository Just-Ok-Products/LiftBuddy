/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { WorkoutplanService } from './workoutplan.service';

describe('Service: WorkoutplanService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [WorkoutplanService]
    });
  });

  it('should ...', inject([WorkoutplanService], (service: WorkoutplanService) => {
    expect(service).toBeTruthy();
  }));
});
