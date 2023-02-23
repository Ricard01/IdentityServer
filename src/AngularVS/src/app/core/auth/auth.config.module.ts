import { NgModule } from '@angular/core';
import { AuthModule, StsConfigLoader, StsConfigStaticLoader } from 'angular-auth-oidc-client';
import { APP_CONFIG } from 'config/app.config';
import { environment } from 'environments/environment';
import { AuthConfigService } from './auth.config.service';
import { AuthService } from './auth.service';

const authFactory = (configService: AuthConfigService) => {
  const config = configService.getConfig();
  return new StsConfigStaticLoader(config);
};
@NgModule({
  imports: [AuthModule.forRoot({
    loader: {
        provide: StsConfigLoader,
        useFactory: authFactory,
        deps: [AuthConfigService]
    }
})],

    exports: [AuthModule],
    providers: [
      { provide: APP_CONFIG, useValue: environment.configuration },
      AuthService
    ]
})
export class AuthConfigModule {}
