import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MyAthletesMainComponent } from './Components/my-athletes-main/my-athletes-main.component';

const routes: Routes = [
  {path: '', component: MyAthletesMainComponent},
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MyAthletesRoutingModule { }
