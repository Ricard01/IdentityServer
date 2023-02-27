import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AutoLoginPartialRoutesGuard } from 'angular-auth-oidc-client';
import { CallbackComponent } from './core/callback/callback.component';
import { LayoutComponent } from './layout/layout.component';


const routes: Routes = [
  { path: '', component:LayoutComponent,
  canActivate:[AutoLoginPartialRoutesGuard],
  children:[
  { path:'', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', loadChildren: () => import('./modules/home/home.module').then(m => m.HomeModule)},
  { path: 'weather', loadChildren: () => import('./modules/weather/weather.module').then(m => m.WeatherModule)},
  { path: 'material/button', loadChildren: () => import('./modules/button/button.module').then(m => m.ButtonModule) },
  { path: 'material/form', loadChildren: () => import('./modules/form/form.module').then(m => m.FormModule) }
  ]} ,
   { path: 'error' , loadChildren: () => import('./modules/errors/error-500/error-500.module').then(m => m.Error500Module)},
  { path: 'callback', component: CallbackComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

