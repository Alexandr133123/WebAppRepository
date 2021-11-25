import { Injectable } from "@angular/core";
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";

@Injectable({
    providedIn: 'root'
})
export class AppRouteGuard implements CanActivate {
    private isCookieExist: boolean | null;

    constructor(private router: Router){ }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        this.isCookieExist = document.cookie.match(/Cookie/) !== null;
        if(this.isCookieExist){
            return true;
        }else{
            this.router.navigate(['login']);
            return false;
        }
        
    }
}