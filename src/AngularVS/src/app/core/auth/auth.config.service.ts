import { Injectable, Inject } from "@angular/core";
import { OpenIdConfiguration, LogLevel } from "angular-auth-oidc-client";
import { APP_CONFIG } from "config/app.config";
import { AppConfig } from "config/app.config.interface";

@Injectable({ providedIn: 'root' })
export class AuthConfigService {
    constructor(
        @Inject(APP_CONFIG) private _config: AppConfig
    ) { }
    getConfig(): OpenIdConfiguration {
        return {
            /* Your config here */
            authority: this._config.AUTHORITY,
            redirectUrl: this._config.REDIRECT_URI,
            postLogoutRedirectUri: window.location.origin,
            clientId: this._config.CLIENT_ID,
            scope: this._config.SCOPE,
            responseType: this._config.RESPONSE_TYPE,
            silentRenew: true,
            useRefreshToken:true,
            secureRoutes: [
                this._config.WEATHER_API_URL,
            ],
            startCheckSession: true,
           // triggerAuthorizationResultEvent: true,
            //renewTimeBeforeTokenExpiresInSeconds:50,
            logLevel: LogLevel.Warn
        };
    }
}
