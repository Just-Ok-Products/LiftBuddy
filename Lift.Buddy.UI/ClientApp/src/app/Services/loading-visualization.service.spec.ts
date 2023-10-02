/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { LoadingVisualizationService } from './loading-visualization.service';

describe('Service: LoadingVisualization', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LoadingVisualizationService]
    });
  });

  it('should ...', inject([LoadingVisualizationService], (service: LoadingVisualizationService) => {
    expect(service).toBeTruthy();
  }));
});
