import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthConfigModule } from './auth/auth.config.module';
import { ErrorsModule } from './errors/errors.module';
import { CallbackComponent } from './callback/callback.component';
import { throwIfAlreadyLoaded } from './theme/functions/module-import-guard';



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
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    throwIfAlreadyLoaded(parentModule, 'CoreModule');
  }
}
