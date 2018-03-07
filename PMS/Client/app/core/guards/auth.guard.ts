import { UrlConstants } from './../common/url.constants';
import { SystemConstants } from './../common/system.constants';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(protected router: Router) { }

    canActivate(activateRoute: ActivatedRouteSnapshot,
        routerState: RouterStateSnapshot) {

        if (localStorage.getItem(SystemConstants.CURRENT_USER)) {
            return true;
        } else {
            this.router.navigate([UrlConstants.LOGIN], {
                queryParams: {
                    returnUrl: routerState.url
                }
            });
            return false;
        }
    }
}