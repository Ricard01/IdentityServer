import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-callback',
  template: '',
  styleUrls: ['./callback.component.scss']
})
export class CallbackComponent implements OnInit, OnDestroy{
  private readonly subscription = new Subscription();
  constructor(  private readonly router: Router, private oidcSecurityService: OidcSecurityService) {}
  ngOnInit(): void {
    this.subscription.add(
    this.oidcSecurityService.checkAuth().subscribe( ({isAuthenticated, userData, accessToken}) => {

      console.log('app authe', isAuthenticated);
      console.log(`current token is '${accessToken}'`);
    })
    );

  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
