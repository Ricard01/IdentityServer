import { BreakpointObserver } from '@angular/cdk/layout';
import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnDestroy, Optional, ViewEncapsulation } from '@angular/core';
import { AppSettings } from 'app/core/theme/models';
import { SettingsService } from 'app/core/theme/services/settings.service';
import { Subscription } from 'rxjs';


const MOBILE_MEDIAQUERY = 'screen and (max-width: 599px)';
const TABLET_MEDIAQUERY = 'screen and (min-width: 600px) and (max-width: 959px)';
const MONITOR_MEDIAQUERY = 'screen and (min-width: 960px)';

@Component({
    selector: 'app-layout',
    templateUrl: './layout.component.html',
    styleUrls: ['./layout.component.scss'],
    encapsulation: ViewEncapsulation.None,

})
export class LayoutComponent implements OnDestroy {


    // @ViewChild('sidenav', { static: true }) sidenav!: MatSidenav;
    // @ViewChild('content', { static: true }) content!: MatSidenavContent;

    sidenavOpened: boolean = true;
    sidenavCollapsed: boolean = false;

    private layoutChangesSubscription: Subscription = new Subscription;

    options = this.settings.getOptions();

    get isOver(): boolean {
        return this.isMobileScreen;
    }

    private isMobileScreen = false;

    // @HostBinding('class.content-width-fix') get contentWidthFix() {
    //   return (
    //     this.isContentWidthFixed &&

    //     this.sidenavOpened &&
    //     !this.isOver
    //   );
    // }

    // private isContentWidthFixed = true;

    private htmlElement!: HTMLHtmlElement;
    constructor(
        private breakpointObserver: BreakpointObserver,
        private settings: SettingsService,
        @Optional() @Inject(DOCUMENT) private document: Document,
    ) {
        this.htmlElement = this.document.querySelector('html')!;
        this.layoutChangesSubscription = this.breakpointObserver
            .observe([MOBILE_MEDIAQUERY, TABLET_MEDIAQUERY, MONITOR_MEDIAQUERY])
            .subscribe(state => {
                // SidenavOpened must be reset true when layout changes
                this.sidenavOpened = true;

                this.isMobileScreen = state.breakpoints[MOBILE_MEDIAQUERY];
                this.sidenavCollapsed = state.breakpoints[TABLET_MEDIAQUERY];
                // this.isContentWidtFixed = state.breakpoints[MONITOR_MEDIAQUERY];

            });
        // Initialize project theme with options
        this.receiveOptions(this.options);
    }

    ngOnDestroy(): void {
        this.layoutChangesSubscription.unsubscribe();
    }


    toggleCollapsed() {
        // this.isContentWidthFixed = false;
        this.sidenavCollapsed = !this.sidenavCollapsed;
    }

    receiveOptions(options: AppSettings): void {
        this.options = options;
        this.settings.setOptions(options);
        this.toggleDarkTheme(options);

    }

    toggleDarkTheme(options: AppSettings) {
        if (options.isDarkTheme) {
            this.htmlElement.classList.remove('light-theme');
        } else {
            this.htmlElement.classList.add('light-theme');
        }
    }
    // onSidenavClosedStart() {
    //   this.isContentWidthFixed = false;
    // }

    onSidenavOpenedChange(isOpened: boolean) {
        // this.isCollapsedWidthFixed = !this.isOver;
        this.sidenavOpened = isOpened;
    }
}

