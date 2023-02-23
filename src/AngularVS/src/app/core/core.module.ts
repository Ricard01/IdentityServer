import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthConfigModule } from './auth/auth.config.module';
import { ErrorsModule } from './errors/errors.module';
import { CallbackComponent } from './callback/callback.component';



@NgModule({
  declarations: [
    CallbackComponent
  ],
  imports: [
    CommonModule,
    AuthConfigModule,
    ErrorsModule
  ]
})
export class CoreModule { }
