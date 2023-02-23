import { Component, EventEmitter, Output, ViewEncapsulation } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { AuthService } from 'app/core/auth/auth.service';

// https://dev.to/shrihari/sign-out-of-all-tabs-if-logged-out-from-one-angular-storage-events-5cdf
@Component({
  selector: 'header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class HeaderComponent {
  @Output() toggleSidenav = new EventEmitter<void>();

  constructor(private authService: AuthService) {}


  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }


  logOut(){
    window.localStorage.setItem('logout-event', Math.random().toString())
    this.authService.logOut();
  }


  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
