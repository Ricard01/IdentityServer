import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Error500Component } from './error-500.component';
import { Route, RouterModule } from '@angular/router';
import { SharedModule } from 'app/shared/shared.module';


const routes: Route[]= [
  { path: '', component: Error500Component}
]

@NgModule({
  declarations: [
    Error500Component
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes)
  ]
})
export class Error500Module { }
