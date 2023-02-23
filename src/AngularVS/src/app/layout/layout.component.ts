import { BreakpointObserver } from '@angular/cdk/layout';
import { Component, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

const MOBILE_MEDIAQUERY = 'screen and (max-width: 599px)';
const TABLET_MEDIAQUERY = 'screen and (min-width: 600px) and (max-width: 959px)';
const MONITOR_MEDIAQUERY = 'screen and (min-width: 960px)';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnDestroy {


  // @ViewChild('sidenav', { static: true }) sidenav!: MatSidenav;
  // @ViewChild('content', { static: true }) content!: MatSidenavContent;

  sidenavOpened: boolean = true;
  sidenavCollapsed: boolean = false;

  private layoutChangesSubscription: Subscription = new Subscription;

  get isOver(): boolean {
    return this.isMobileScreen;
  }

  private isMobileScreen = false;

  // @HostBinding('class.content-width-fix') get contentWidthFix() {
  //   return (
  //     this.isContentWidthFixed &&

  //     this.sidenavOpened &&
  //     !this.isOver
  //   );
  // }

  // private isContentWidthFixed = true;


  constructor(
    private breakpointObserver: BreakpointObserver,
  ) {

    this.layoutChangesSubscription = this.breakpointObserver
      .observe([MOBILE_MEDIAQUERY, TABLET_MEDIAQUERY, MONITOR_MEDIAQUERY])
      .subscribe(state => {
        // SidenavOpened must be reset true when layout changes
        this.sidenavOpened = true;

        this.isMobileScreen = state.breakpoints[MOBILE_MEDIAQUERY];
        this.sidenavCollapsed = state.breakpoints[TABLET_MEDIAQUERY];
        // this.isContentWidtFixed = state.breakpoints[MONITOR_MEDIAQUERY];

      });

  }

  ngOnDestroy(): void {
    this.layoutChangesSubscription.unsubscribe();
  }


  toggleCollapsed() {
    // this.isContentWidthFixed = false;
    this.sidenavCollapsed = !this.sidenavCollapsed;
  }


  // onSidenavClosedStart() {
  //   this.isContentWidthFixed = false;
  // }

  onSidenavOpenedChange(isOpened: boolean) {
    // this.isCollapsedWidthFixed = !this.isOver;
    this.sidenavOpened = isOpened;
  }
}
