import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyAthletesMainComponent } from './Components/my-athletes-main/my-athletes-main.component';
import { MyAthletesRoutingModule } from './my-athletes-routing.module';
import { UIElementsModule } from '../Components/UI.Elements.module';
import { RouterModule } from '@angular/router';
import { MyAthletesComponent } from './my-athletes.component';
import { BrowserModule } from '@angular/platform-browser';
import { MyAthletesRightMenuComponent } from './Components/my-athletes-right-menu/my-athletes-right-menu.component';

@NgModule({
  imports: [
    CommonModule,
    MyAthletesRoutingModule,
    BrowserModule,
    RouterModule,
    UIElementsModule
  ],
  declarations: [
    MyAthletesComponent,
    MyAthletesMainComponent,
    MyAthletesRightMenuComponent
  ]
})
export class MyAthletesModule { }
