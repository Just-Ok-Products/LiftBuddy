/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PersonalRecordComponent } from './pr.component';

describe('PrComponent', () => {
    let component: PersonalRecordComponent;
    let fixture: ComponentFixture<PersonalRecordComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [PersonalRecordComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(PersonalRecordComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
