import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginContainerComponent } from './Pages/Login/login.component';
import { LoginPageComponent } from './Pages/Login/Components/login-page/login-page.component';
import { RegisterPageComponent } from './Pages/Login/Components/register-page/register-page.component';
import { HomePageComponent } from './Pages/Home/home-page.component';
import { ForgotPasswordPageComponent } from './Pages/Login/Components/forgot-password-page/forgot-password-page.component';
import { WorkoutPlansComponent } from './Pages/WorkoutPlans/workout-plans.component';
import { UserDataComponent } from './Pages/UserData/user-data.component';
import { AuthGuard } from './Services/Guards/AuthGuard';
import { PersonalRecordComponent } from './Pages/PersonalRecords/pr.component';
import { SettingsComponent } from './Pages/Settings/settings.component';
import { SettingsRoutingModule } from './Pages/Settings/setings-routing.module';
import { WorkoutPlansRoutingModule } from './Pages/WorkoutPlans/workout-plans-routing.module';
import { LoginRoutingModule } from './Pages/Login/login-routing.module';
import { PageNotFoundComponent } from './Pages/PageNotFound/page-not-found.component';

const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'user', component: UserDataComponent, canActivate: [AuthGuard] },
    { path: 'home', component: HomePageComponent },
    { path: 'pr', component: PersonalRecordComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginContainerComponent, loadChildren: () => LoginRoutingModule },
    { path: 'workout', component: WorkoutPlansComponent, loadChildren: () => WorkoutPlansRoutingModule, canActivate: [AuthGuard] },
    { path: 'settings', component: SettingsComponent, loadChildren: () => SettingsRoutingModule },
    { path: '**', component: PageNotFoundComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
