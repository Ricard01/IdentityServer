import { Component, Input } from '@angular/core';
import { MenuService } from 'app/core/theme/services/menu.service';



@Component({
  selector: 'app-sidemenu',
  templateUrl: './sidemenu.component.html',
  styleUrls: ['./sidemenu.component.scss']
})
export class SidemenuComponent {
   // Note: Ripple effect make page flashing on mobile
  @Input() ripple = false;

  menu$ = this.menu.getAll();

  buildRoute = this.menu.buildRoute;

  constructor(private menu: MenuService) {}
}
