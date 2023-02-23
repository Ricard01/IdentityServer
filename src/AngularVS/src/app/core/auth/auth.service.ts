import { Injectable, OnDestroy } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnDestroy {

  isLoggedIn: boolean = false

  constructor(private oidcSecurityService: OidcSecurityService){
    console.log('authService Starts')
    this.start()
  }

  logOut() {
    this.oidcSecurityService.logoff()
      .subscribe((result) => {
        console.log(result);

      });
  }

  logOutAndRevoket(){
    this.oidcSecurityService.logoffAndRevokeTokens()
.subscribe((result)=> console.log(result));
  }

   // Bind the eventListener
   private start(): void {
    window.addEventListener("storage", this.storageEventListener.bind(this));
  }

  // Logout only when key is 'logout-event'
  private storageEventListener(event: StorageEvent) {
    if (event.storageArea == localStorage) {
      if (event?.key && event.key == 'logout-event') {
        console.log("🔥 ~ storageEventListener ~ event", event.newValue)
        this.logOut()
      }
    }
  }

  // Handle active listeners when onDestroy
  private stop(): void {
    window.removeEventListener("storage", this.storageEventListener.bind(this));
  }

  ngOnDestroy() {
    this.stop()
  }


}
