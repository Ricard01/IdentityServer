import { Component, EventEmitter, HostBinding, OnDestroy, OnInit, Output, ViewEncapsulation } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { AuthService } from 'app/core/auth/auth.service';
import { AppSettings } from 'app/core/theme/models';
import { SettingsService } from 'app/core/theme/services/settings.service';
import { Subscription } from 'rxjs';

// https://dev.to/shrihari/sign-out-of-all-tabs-if-logged-out-from-one-angular-storage-events-5cdf
@Component({
  selector: 'header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class HeaderComponent implements OnInit, OnDestroy {
    
    @Output() toggleSidenav = new EventEmitter<void>();
    @Output() optionsChange = new EventEmitter<AppSettings>();
    @HostBinding('class') className = '';

    isExpanded = false;

    options = this.settings.getOptions();

    // declaro el tema por default como dark 
    form = this.fb.nonNullable.group<AppSettings>({
        isDarkTheme: true,
        language: 'es-MX'
    });

    formSubscription = Subscription.EMPTY;

    private readonly key = 'page-settings';

    constructor(private settings: SettingsService, private fb: FormBuilder, private authService: AuthService) { }

    ngOnInit(): void {

        this.form.patchValue(this.options);

        this.formSubscription = this.form.valueChanges.subscribe(value => {
            this.sendOptions(value as AppSettings);
        });
    }

    logOut(){
      window.localStorage.setItem('logout-event', Math.random().toString())
      this.authService.logOut();
    }

    sendOptions(options: AppSettings) {
        this.optionsChange.emit(options);
    }

    collapse() {
        this.isExpanded = false;
    }

    toggle() {
        this.isExpanded = !this.isExpanded;
    }

    ngOnDestroy(): void {
        this.formSubscription.unsubscribe();
    }




}
