import { NgModule } from '@angular/core';
import { AuthModule, LogLevel } from 'angular-auth-oidc-client';


@NgModule({
    imports: [AuthModule.forRoot({
        config: {
              authority: 'https://localhost:5000',
              redirectUrl: 'https://localhost:5003/callback',
            //   postLogoutRedirectUri: window.location.origin,
              clientId: 'angularCmd',
              scope: 'openid profile offline_access weatherapi.read', // 'openid profile offline_access ' + your scopes
              responseType: 'code',
              silentRenew: true,
              useRefreshToken: true,
              logLevel: LogLevel.Debug,
              //renewTimeBeforeTokenExpiresInSeconds: 30,
          }
      })],
    exports: [AuthModule],
})
export class AuthConfigModule {}
