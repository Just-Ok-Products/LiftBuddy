import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SearchResultComponent } from './Components/search-result/search-result.component';
import { EmptySearchComponent } from './Components/empty-search/empty-search.component';

const routes: Routes = [
  {path: '', component: EmptySearchComponent},
  {path: '**', redirectTo: ''},
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SearchRoutingModule { }
