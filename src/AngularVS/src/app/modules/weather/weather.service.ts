import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { APP_CONFIG } from 'config/app.config';
import { AppConfig } from 'config/app.config.interface';
import { IWeather } from './models/weather';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {

  private _weatherUrl =`${this._config.WEATHER_API_URL}/WeatherForecast`;

  constructor(
    private httpClient: HttpClient,
    @Inject(APP_CONFIG) private _config: AppConfig) { }

    getWheater() {
      return this.httpClient.get<IWeather[]>(this._weatherUrl)
    }

}
