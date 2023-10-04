import { CreateUpdateWorkoutplanPageComponent } from './Components/create-update-workoutplan-page/create-update-workoutplan-page.component';
import { YourWorkoutsPageComponent } from './Components/your-workouts-page/your-workouts-page.component';
import { MyWorkoutsComponent } from './Components/my-workouts/my-workouts.component';
import { WorkoutNavigationMenuComponent } from '../Components/workout-navigation-menu/workout-navigation-menu.component';
import { UIElementsModule } from '../Components/UI.Elements.module';
import { WorkoutPlansComponent } from './workout-plans.component';
import { DeleteWorkoutPlanConfirmationPopupComponent } from './Components/my-workouts/Components/delete-workout-plan-confirmation-popup/delete-workout-plan-confirmation-popup.component';
import { TrainingCardComponent } from './Components/your-workouts-page/Components/training-card/training-card.component';
import { ExerciseRowComponent } from './Components/create-update-workoutplan-page/Components/daily-workout/Components/exercise-row/exercise-row.component';
import { DailyWorkoutComponent } from './Components/create-update-workoutplan-page/Components/daily-workout/daily-workout.component';
import { WorkoutPlansRoutingModule } from './workout-plans-routing.module';

import { MatFormFieldModule } from '@angular/material/form-field';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatOptionModule } from '@angular/material/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

@NgModule({
  imports: [
    CommonModule,
    UIElementsModule,
    ReactiveFormsModule,
    FormsModule,
    MatCardModule,
    MatIconModule,
    MatFormFieldModule,
    MatOptionModule,
    BrowserModule,
    RouterModule,
    MatButtonModule,
    MatInputModule,
    MatSelectModule,
    WorkoutPlansRoutingModule
  ],
  declarations: [
    WorkoutPlansComponent,
    YourWorkoutsPageComponent,
    CreateUpdateWorkoutplanPageComponent,
    MyWorkoutsComponent,
    WorkoutNavigationMenuComponent,
    DeleteWorkoutPlanConfirmationPopupComponent,
    TrainingCardComponent,
    ExerciseRowComponent,
    DailyWorkoutComponent
  ],
  exports: [

  ]
})
export class WorkoutPlansModule { }
