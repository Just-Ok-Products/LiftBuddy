/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PersonalRecordService } from './pr.service';

describe('Service: Pr', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [PersonalRecordService]
        });
    });

    it('should ...', inject([PersonalRecordService], (service: PersonalRecordService) => {
        expect(service).toBeTruthy();
    }));
});
