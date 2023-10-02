import { WorkoutDay } from 'src/app/Model/WorkoutDay';
import { Component, Input, OnInit } from '@angular/core';

@Component({
    selector: 'app-training-card',
    templateUrl: './training-card.component.html',
    styleUrls: ['./training-card.component.css']
})

export class TrainingCardComponent implements OnInit {

    @Input() workoutDay: WorkoutDay = new WorkoutDay();
    @Input() name: string = '';

    constructor() { }

    ngOnInit() {
    }

}
