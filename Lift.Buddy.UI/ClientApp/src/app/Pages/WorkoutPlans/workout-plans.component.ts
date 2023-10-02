import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-workout-plans',
    templateUrl: './workout-plans.component.html',
    styleUrls: ['./workout-plans.component.css']
})

export class WorkoutPlansComponent implements OnInit {

    public items = [1, 2, 3, 4]

    constructor() { }

    ngOnInit() { }

}
