/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PrService } from './pr.service';

describe('Service: Pr', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PrService]
    });
  });

  it('should ...', inject([PrService], (service: PrService) => {
    expect(service).toBeTruthy();
  }));
});
