import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginContainerComponent } from './Pages/login/login-container.component';
import { LoginPageComponent } from './Pages/login/Components/login-page/login-page.component';
import { RegisterPageComponent } from './Pages/login/Components/register-page/register-page.component';
import { HomePageComponent } from './Pages/Home/home-page.component';
import { ForgotPasswordPageComponent } from './Pages/login/Components/forgot-password-page/forgot-password-page.component';
import { WorkoutPlansComponent } from './Pages/WorkoutPlans/workout-plans.component';
import { YourWorkoutsPageComponent } from './Pages/WorkoutPlans/Components/your-workouts-page/your-workouts-page.component';
import { CreateUpdateWorkoutplanPageComponent } from './Pages/WorkoutPlans/Components/create-update-workoutplan-page/create-update-workoutplan-page.component';
import { DailyWorkoutComponent } from './Pages/WorkoutPlans/Components/create-update-workoutplan-page/Components/daily-workout/daily-workout.component';
import { UserDataComponent } from './Pages/UserData/user-data.component';
import { AuthGuard } from './Services/Guards/AuthGuard';
import { PrComponent } from './Pages/PR/pr.component';

const routes: Routes = [
  {path: '', redirectTo: 'home', pathMatch: 'full'},
  {path: 'user', component: UserDataComponent, canActivate: [AuthGuard]},
  {path: 'home', component: HomePageComponent},
  {path: 'pr', component: PrComponent, canActivate: [AuthGuard]},
  {
    path: 'login',
    component: LoginContainerComponent,
    children: [
      {path: '', component: LoginPageComponent},
      {path: 'register', component: RegisterPageComponent},
      {path: 'forgot-password', component: ForgotPasswordPageComponent}
    ]
  },
  {
    path: 'workouts',
    component: WorkoutPlansComponent,
    children: [
      {path: 'home', component: YourWorkoutsPageComponent},
      {
        path: 'add/:workoutId',
        component: CreateUpdateWorkoutplanPageComponent,
        children: [

          {path: ':workoutId', component: DailyWorkoutComponent}
        ]
      },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
