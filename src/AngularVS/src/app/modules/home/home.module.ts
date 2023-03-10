import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Route, RouterModule } from '@angular/router';
import { HomeComponent } from './home.component';
import { SharedModule } from 'app/shared/shared.module';

const routes: Route[] =[
  {path: '', component: HomeComponent}
]

@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    CommonModule,
   
    RouterModule.forChild(routes),
  ]
})
export class HomeModule { }
