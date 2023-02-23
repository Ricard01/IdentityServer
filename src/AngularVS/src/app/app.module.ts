import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { CallbackComponent } from './core/callback/callback.component';
import { AppRoutingModule } from './app-routing.module';
import { AuthConfigModule } from './core/auth/auth.config.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MyLayoutModule } from './layout/layout.module';
import { SharedModule } from './shared/shared.module';
import { CoreModule } from './core/core.module';
import { PublicEventsService, EventTypes, AuthInterceptor } from 'angular-auth-oidc-client';
import { filter } from 'rxjs';
import { HTTP_INTERCEPTORS } from '@angular/common/http';




@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule,
    BrowserAnimationsModule,
    MyLayoutModule,
    SharedModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(private eventService: PublicEventsService) {
    this.eventService
      .registerForEvents()
      .pipe(filter((notification) => notification.type === EventTypes.CheckSessionReceived))
      .subscribe((value) => console.log('CheckSessionChanged with value', value));
  }
 }
