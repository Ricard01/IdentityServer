import { AppConfig } from "./app.config.interface";

export const LOCALHOST_CONFIG: AppConfig = {
    AUTHORITY: 'https://localhost:5000',
    REDIRECT_URI: `${window.location.origin}/callback`,
    POST_LOGOUT_REDIRECT_URI: `${window.location.origin}`,
    CLIENT_ID: 'angularVS',
    RESPONSE_TYPE: 'code',
    SCOPE: 'openid offline_access weatherapi.write ',
    WEATHER_API_URL: 'https://localhost:5001',
    SECURE_ROUTES: [ 'https://localhost:5001' ],
}
