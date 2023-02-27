import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, switchMap, tap } from 'rxjs/operators';
import { Menu } from '../models';
// import { NgxPermissionsService, NgxRolesService } from 'ngx-permissions';
// import { AuthService, User } from '@core/authentication';
import {  MenuService } from './menu.service';



@Injectable({
  providedIn: 'root',
})
export class StartupService {
  constructor(
    // private authService: AuthService,
    private http: HttpClient, // Agregado;
    private menuService: MenuService,
    // private permissonsService: NgxPermissionsService,
    // private rolesService: NgxRolesService
  ) { }


  menu() {
    return this.http.get<{ menu: Menu[] }>('../../../assets/data/menu.json').pipe(map(res => res.menu));
  }
  /**
   * Load the application only after get the menu or other essential informations
   * such as permissions and roles.
   */

  load() {
    return new Promise<void>((resolve, reject) => {

      this.menu().pipe(
        // tap(menu => console.log(menu)),
        map(menu => this.setMenu(menu))
      ).subscribe(
        () => resolve(),
      
      );
    })
  }

  // load(){
  //   this.setMenu(menu)
  // }

  private setMenu(menu: Menu[]) {
    this.menuService.addNamespace(menu, 'menu');
    this.menuService.set(menu);
  }

  // private setPermissions(user: User) {
  //   // In a real app, you should get permissions and roles from the user information.
  //   const permissions = ['canAdd', 'canDelete', 'canEdit', 'canRead'];
  //   this.permissonsService.loadPermissions(permissions);
  //   this.rolesService.flushRoles();
  //   this.rolesService.addRoles({ ADMIN: permissions });

  //   // Tips: Alternatively you can add permissions with role at the same time.
  //   // this.rolesService.addRolesWithPermissions({ ADMIN: permissions });
  // }
}
