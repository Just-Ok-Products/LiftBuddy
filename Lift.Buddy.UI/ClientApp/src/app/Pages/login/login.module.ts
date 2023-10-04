import { LoginComponent } from "./login.component";
import { RegisterPageComponent } from './Components/register-page/register-page.component';
import { UserInformationComponent } from './Components/register-page/Components/user-information/user-information.component';
import { ForgotPasswordPageComponent } from './Components/forgot-password-page/forgot-password-page.component';
import { SecurityQuestionsComponent } from './Components/register-page/Components/security-questions/security-questions.component';

import { MatCardModule } from '@angular/material/card';
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { RouterModule } from '@angular/router';
import { MatOptionModule } from '@angular/material/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { UIElementsModule } from '../Components/UI.Elements.module';
import { LoginPageComponent } from './Components/login-page/login-page.component';

@NgModule({
    imports: [
        BrowserModule,
        MatCardModule,
        ReactiveFormsModule,
        FormsModule,
        MatFormFieldModule,
        RouterModule,
        MatOptionModule,
        MatButtonModule,
        MatInputModule,
        MatIconModule,
        MatSelectModule,
        UIElementsModule
    ],
    declarations: [
        LoginPageComponent,
        LoginComponent,
        RegisterPageComponent,
        ForgotPasswordPageComponent,
        UserInformationComponent,
        SecurityQuestionsComponent
    ],
    exports: [
      LoginComponent
    ],
    bootstrap: []
})
export class LoginModule { }
