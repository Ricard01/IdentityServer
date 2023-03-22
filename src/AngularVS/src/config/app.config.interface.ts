export interface AppConfig {
  AUTHORITY: string;
  REDIRECT_URI: string;
  POST_LOGOUT_REDIRECT_URI: string;
  CLIENT_ID: string;
  SCOPE: string;
  RESPONSE_TYPE: string;
  WEATHER_API_URL: string;
  IDENTITY_API_URL: string;
  SECURE_ROUTES:string[];
}
