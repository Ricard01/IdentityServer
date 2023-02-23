import { InjectionToken } from '@angular/core';
import { AppConfig } from './app.config.interface';

// The Injection Token allows creating token that allows the injection of values that don’t have a runtime representation.
// we create the Injection Token by creating a new instance of the InjectionToken class. They ensure that the tokens are always unique.
export const APP_CONFIG = new InjectionToken<AppConfig>('app.config');
