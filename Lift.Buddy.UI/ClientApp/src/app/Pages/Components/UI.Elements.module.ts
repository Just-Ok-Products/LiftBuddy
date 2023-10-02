import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PageStructureComponent } from '../Components/page-structure/page-structure.component';
import { LeftMenuComponent } from '../Components/left-menu/left-menu.component';
import { HeaderComponent } from './header/header.component';
import { NavigationMenuComponent } from './navigation-menu/navigation-menu.component';
import { StarsRatingComponent } from './stars-rating/stars-rating.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTreeModule } from '@angular/material/tree';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { MatButtonModule } from '@angular/material/button';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { MatCardModule } from '@angular/material/card';
import { LoadingSpinnerComponent } from './loading-spinner/loading-spinner.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@NgModule({
  imports: [
    CommonModule,
    MatToolbarModule,
    MatTreeModule,
    AppRoutingModule,
    MatButtonModule,
    MatCardModule,
    MatProgressSpinnerModule
  ],
  declarations: [
    HeaderComponent,
    LeftMenuComponent,
    NavigationMenuComponent,
    PageStructureComponent,
    StarsRatingComponent,
    ConfirmationDialogComponent,
    LoadingSpinnerComponent
  ],
  exports: [
    HeaderComponent,
    LeftMenuComponent,
    NavigationMenuComponent,
    PageStructureComponent,
    StarsRatingComponent,
    ConfirmationDialogComponent,
    LoadingSpinnerComponent
  ]
})
export class UIElementsModule { }
