import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from './core/core.module';
import { LayoutModule} from './layout/layout.module';
import { SharedModule } from './shared/shared.module';
import { appInitializerProviders } from './core/theme/functions';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { PublicEventsService, EventTypes, AuthInterceptor } from 'angular-auth-oidc-client';
import { filter } from 'rxjs';




@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    CoreModule,
    LayoutModule,
    SharedModule
  ],
  providers: [
    // Ejecuta un servicio para crear el "SideMenu" a partir de un archivo JSON.
    appInitializerProviders,
    // Intercept any outgoing HTTP request and add an authorization header. You can configure the routes you want to send a token within the configuration: AuthModule.forRoot({config: { secureRoutes: ...     
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }, //  
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(private eventService: PublicEventsService) {
    // this.eventService
    //   .registerForEvents()
    //   .pipe(filter((notification) => notification.type === EventTypes.CheckSessionReceived))
    //   .subscribe((value) => console.log('CheckSessionChanged with value', value));
  }
 }
