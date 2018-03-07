import { AuthGuard } from './auth.guard';
import { UrlConstants } from './../common/url.constants';
import { SystemConstants } from './../common/system.constants';
import { AuthenService } from './../services/authen.service';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';


@Injectable()
export class StudentAuthGuard extends AuthGuard {

    constructor(router : Router) {
        super(router);
    }

    canActivate(activateRoute: ActivatedRouteSnapshot,
        routerState: RouterStateSnapshot) {
        var user = JSON.parse(localStorage.getItem(SystemConstants.CURRENT_USER));
        if (user.role === "Student") {
            return true;
        } else {
            this.router.navigate([UrlConstants.HOME], {
                queryParams: {
                    returnUrl: routerState.url
                }
            });
            return false;
        }
    }
}