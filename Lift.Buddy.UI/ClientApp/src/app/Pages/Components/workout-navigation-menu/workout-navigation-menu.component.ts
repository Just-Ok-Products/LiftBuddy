import { Component, OnInit } from '@angular/core';
import { NavigationMenu } from 'src/app/Model/NavigationMenu';
import { workoutNavigationMenu } from '../../../routes'

@Component({
    selector: 'app-workout-navigation-menu',
    templateUrl: './workout-navigation-menu.component.html',
    styleUrls: ['./workout-navigation-menu.component.css']
})

export class WorkoutNavigationMenuComponent implements OnInit {

    constructor() { }

    ngOnInit() {
    }

    public navigationData: NavigationMenu[] = workoutNavigationMenu
}
