import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { APP_CONFIG } from 'config/app.config';
import { AppConfig } from 'config/app.config.interface';


@Injectable({
    providedIn: 'root'
  })
  export class HomeService {
  
    private _claimsUrl =`${this._config.IDENTITY_API_URL}/Claims`;
    private _usersUrl =`${this._config.IDENTITY_API_URL}/Users`;
  
    constructor(
      private httpClient: HttpClient,
      @Inject(APP_CONFIG) private _config: AppConfig) { }
  
      getClaims() {
        return this.httpClient.get<string[]>(this._claimsUrl)
      }

      getUsers()
      {
        return this.httpClient.get(this._usersUrl)
      }
  
  }