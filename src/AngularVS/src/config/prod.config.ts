import { AppConfig } from "./app.config.interface";

export const PROD_CONFIG: AppConfig = {
  AUTHORITY: 'https://localhost:5000',
  REDIRECT_URI: `${window.location.origin}/callback`,
  POST_LOGOUT_REDIRECT_URI: `${window.location.origin}`,
  CLIENT_ID: 'angularVS',
  RESPONSE_TYPE: 'code',
  SCOPE: 'openid offline_access identity.api',
  WEATHER_API_URL: 'https://localhost:5001',
  IDENTITY_API_URL: 'https://localhost:4000',
  SECURE_ROUTES: [ 'https://localhost:5001' ],


}
