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
//#endregion

//#region Services
import { LoginService } from './Pages/login/Services/login.service';
import { ApiCallsService } from './Services/Utils/api-calls.service';
//#endregion

//#region Components
import { LoginPageComponent } from './Pages/login/Components/login-page/login-page.component';
import { LoginContainerComponent } from './Pages/login/login-container.component';
import { RegisterPageComponent } from './Pages/login/Components/register-page/register-page.component';
import { ForgotPasswordPageComponent } from './Pages/login/Components/forgot-password-page/forgot-password-page.component';
import { HomePageComponent } from './Pages/Home/home-page/home-page.component';
import { UserInformationComponent } from './Pages/login/Components/register-page/Components/user-information/user-information.component';
import { SecurityQuestionsComponent } from './Pages/login/Components/register-page/Components/security-questions/security-questions.component';
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
    MatSelectModule
  ],
  providers: [LoginService, ApiCallsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
