import { Component, OnInit } from '@angular/core';
import { IWeather } from './models/weather';
import { WeatherService } from './weather.service';

@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html',
  styleUrls: ['./weather.component.scss']
})
export class WeatherComponent implements OnInit {

  weather: IWeather[] | undefined;

  constructor(private weatherService: WeatherService) {

 
  }
  ngOnInit(): void {
    this.getWeather();
  }

  getWeather() {
    this.weatherService.getWheater()
    .subscribe( weather => this.weather = weather);     
  }


}
