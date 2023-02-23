import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WeatherComponent } from './weather.component';
import { Route, RouterModule } from '@angular/router';

const routes: Route[] = [
  { path: '', component: WeatherComponent}
]

@NgModule({
  declarations: [
    WeatherComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class WeatherModule { }
