import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

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
import { MatChipsModule } from '@angular/material/chips'
import { MatAutocompleteModule } from '@angular/material/autocomplete'
//#endregion

import { ScrollingModule } from '@angular/cdk/scrolling'

//#region Services
import { LoginService } from './Services/login.service';
import { ApiCallsService } from './Services/Utils/api-calls.service';
//#endregion

//#region Components
import { LoginPageComponent } from './Pages/Login/Components/login-page/login-page.component';
import { LoginContainerComponent } from './Pages/Login/login.component';
import { RegisterPageComponent } from './Pages/Login/Components/register-page/register-page.component';
import { ForgotPasswordPageComponent } from './Pages/Login/Components/forgot-password-page/forgot-password-page.component';
import { HomePageComponent } from './Pages/Home/home-page.component';
import { UserInformationComponent } from './Pages/Login/Components/register-page/Components/user-information/user-information.component';
import { SecurityQuestionsComponent } from './Pages/Login/Components/register-page/Components/security-questions/security-questions.component';
import { WorkoutPlansModule } from './Pages/WorkoutPlans/workout-plans.module';
import { WorkoutPlansComponent } from './Pages/WorkoutPlans/workout-plans.component';
import { CreateUpdateWorkoutplanPageComponent } from './Pages/WorkoutPlans/Components/create-update-workoutplan-page/create-update-workoutplan-page.component';
import { YourWorkoutsPageComponent } from './Pages/WorkoutPlans/Components/your-workouts-page/your-workouts-page.component';
import { ExerciseRowComponent } from './Pages/WorkoutPlans/Components/create-update-workoutplan-page/Components/daily-workout/Components/exercise-row/exercise-row.component';
import { DailyWorkoutComponent } from './Pages/WorkoutPlans/Components/create-update-workoutplan-page/Components/daily-workout/daily-workout.component';
import { UserDataComponent } from './Pages/UserData/user-data.component';
import { UserDataFormComponent } from './Pages/UserData/Components/user-data-form/user-data-form.component';
import { PersonalRecordComponent } from './Pages/PersonalRecords/pr.component';
import { PrMenuComponent } from './Pages/PersonalRecords/Components/pr-menu/pr-menu.component';
import { PrExerciseComponent } from './Pages/PersonalRecords/Components/pr-exercise/pr-exercise.component';
import { PersonalRecordService } from './Services/pr.service';
import { TrainingCardComponent } from './Pages/WorkoutPlans/Components/your-workouts-page/Components/training-card/training-card.component';
import { MyWorkoutsComponent } from './Pages/WorkoutPlans/Components/my-workouts/my-workouts.component';
import { DialogService } from './Services/Utils/dialog.service';
import { DeleteWorkoutPlanConfirmationPopupComponent } from './Pages/WorkoutPlans/Components/my-workouts/Components/delete-workout-plan-confirmation-popup/delete-workout-plan-confirmation-popup.component';
import { MatDialogModule } from '@angular/material/dialog';
import { SettingsModule } from './Pages/Settings/settings.module';
import { UIElementsModule } from './Pages/Components/UI.Elements.module';
import { LoginModule } from './Pages/Login/login.module';
import { PageNotFoundModule } from './Pages/PageNotFound/page-not-found.module';
import { NavigationPageCardComponent } from './Pages/Home/Components/navigation-page-card/navigation-page-card.component';
//#endregion

@NgModule({
    declarations: [
        AppComponent,
        HomePageComponent,
        UserDataComponent,
        UserDataFormComponent,
        PersonalRecordComponent,
        PrMenuComponent,
        PrExerciseComponent,
        NavigationPageCardComponent
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
        ScrollingModule,
        MatCheckboxModule,
        MatChipsModule,
        MatAutocompleteModule,
        MatDialogModule,
        WorkoutPlansModule,
        SettingsModule,
        UIElementsModule,
        LoginModule,
        PageNotFoundModule
    ],
    providers: [LoginService, ApiCallsService, PersonalRecordService, DialogService],
    bootstrap: [AppComponent]
})
export class AppModule { }
