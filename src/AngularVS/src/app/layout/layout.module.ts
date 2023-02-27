import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { SharedModule } from '../shared/shared.module';
import { RouterModule } from '@angular/router';
import { LayoutComponent } from './layout.component';
import { NavAccordionDirective, NavAccordionItemDirective, NavAccordionToggleDirective } from './sidemenu/directives';
import { SidemenuComponent } from './sidemenu/sidemenu.component';



@NgModule({
  declarations: [
    HeaderComponent,
    SidebarComponent,
    LayoutComponent,
    SidemenuComponent,
    NavAccordionDirective,
    NavAccordionItemDirective,
    NavAccordionToggleDirective,
  ],
  imports: [    
    RouterModule,
    SharedModule
  ],
  exports:[
    HeaderComponent,
    SidebarComponent
  ]
})
export class LayoutModule { }
