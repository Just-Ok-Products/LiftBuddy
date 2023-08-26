import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

//#region Material imports
import { MatButtonModule } from '@angular/material/button'
import { MatCardModule } from '@angular/material/card'
import { MatFormFieldModule } from '@angular/material/form-field'
import { MatInputModule } from '@angular/material/input'
import { MatSnackBarModule } from '@angular/material/snack-bar'
import { MatIconModule } from '@angular/material/icon'
import { MatGridListModule } from '@angular/material/grid-list'
import { MatSelectModule } from '@angular/material/select'
import { MatToolbarModule } from '@angular/material/toolbar'
import { MatListModule } from '@angular/material/list'
import { MatTreeModule } from '@angular/material/tree'
import { MatCheckboxModule } from '@angular/material/checkbox'
//#endregion

import { ScrollingModule } from '@angular/cdk/scrolling'

//#region Services
import { LoginService } from './Services/login.service';
import { ApiCallsService } from './Services/Utils/api-calls.service';
//#endregion

//#region Components
import { LoginPageComponent } from './Pages/login/Components/login-page/login-page.component';
import { LoginContainerComponent } from './Pages/login/login-container.component';
import { RegisterPageComponent } from './Pages/login/Components/register-page/register-page.component';
import { ForgotPasswordPageComponent } from './Pages/login/Components/forgot-password-page/forgot-password-page.component';
import { HomePageComponent } from './Pages/Home/home-page.component';
import { UserInformationComponent } from './Pages/login/Components/register-page/Components/user-information/user-information.component';
import { SecurityQuestionsComponent } from './Pages/login/Components/register-page/Components/security-questions/security-questions.component';
import { LeftMenuComponent } from './Pages/Components/left-menu/left-menu.component';
import { WorkoutPlansModule } from './Pages/WorkoutPlans/workout-plans.module';
import { HeaderComponent } from './Pages/Components/header/header.component';
import { WorkoutPlansComponent } from './Pages/WorkoutPlans/workout-plans.component';
import { CreateUpdateWorkoutplanPageComponent } from './Pages/WorkoutPlans/Components/create-update-workoutplan-page/create-update-workoutplan-page.component';
import { YourWorkoutsPageComponent } from './Pages/WorkoutPlans/Components/your-workouts-page/your-workouts-page.component';
import { PageStructureComponent } from './Pages/Components/page-structure/page-structure.component';
import { ExerciseRowComponent } from './Pages/WorkoutPlans/Components/create-update-workoutplan-page/Components/daily-workout/Components/exercise-row/exercise-row.component';
import { DailyWorkoutComponent } from './Pages/WorkoutPlans/Components/create-update-workoutplan-page/Components/daily-workout/daily-workout.component';
import { UserDataComponent } from './Pages/UserData/user-data.component';
import { UserDataFormComponent } from './Pages/UserData/Components/user-data-form/user-data-form.component';
import { PrComponent } from './Pages/PR/pr.component';
import { PrMenuComponent } from './Pages/PR/Components/pr-menu/pr-menu.component';
import { PrExerciseComponent } from './Pages/PR/Components/pr-exercise/pr-exercise.component';
import { PrService } from './Services/pr.service';
//#endregion

@NgModule({
  declarations: [
    AppComponent,
    LoginPageComponent,
    LoginContainerComponent,
    RegisterPageComponent,
    HomePageComponent,
    ForgotPasswordPageComponent,
    UserInformationComponent,
    SecurityQuestionsComponent,
    LeftMenuComponent,
    HeaderComponent,
    WorkoutPlansComponent,
    CreateUpdateWorkoutplanPageComponent,
    YourWorkoutsPageComponent,
    PageStructureComponent,
    ExerciseRowComponent,
    DailyWorkoutComponent,
    UserDataComponent,
    UserDataFormComponent,
    PrComponent,
    PrMenuComponent,
    PrExerciseComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatCardModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    FormsModule,
    MatSnackBarModule,
    MatIconModule,
    MatGridListModule,
    MatSelectModule,
    MatToolbarModule,
    MatListModule,
    MatTreeModule,
    WorkoutPlansModule,
    ScrollingModule,
    MatCheckboxModule
  ],
  providers: [LoginService, ApiCallsService, PrService],
  bootstrap: [AppComponent]
})
export class AppModule { }
