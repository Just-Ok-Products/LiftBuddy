import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchComponent } from './search.component';
import { SearchRoutingModule } from './search-routing.module';
import { UIElementsModule } from '../Components/UI.Elements.module';
import { SearchResultComponent } from './Components/search-result/search-result.component';
import { EmptySearchComponent } from './Components/empty-search/empty-search.component';

import { MatToolbarModule } from '@angular/material/toolbar'
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatCardModule } from '@angular/material/card';

@NgModule({
  imports: [
    CommonModule,
    SearchRoutingModule,
    UIElementsModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatAutocompleteModule,
    MatCardModule
  ],
  declarations: [
    SearchComponent,
    SearchResultComponent,
    EmptySearchComponent
  ]
})
export class SearchModule { }
