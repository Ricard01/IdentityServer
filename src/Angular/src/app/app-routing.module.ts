import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AutoLoginPartialRoutesGuard } from 'angular-auth-oidc-client';
import { CallbackComponent } from './callback/callback.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {path: '', pathMatch: 'full', redirectTo: 'home'},  
  {path: 'home', component: HomeComponent, canActivate: [AutoLoginPartialRoutesGuard] },
  {path: 'callback', component: CallbackComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
