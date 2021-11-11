import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Subject } from "rxjs";

@Injectable({
    providedIn: 'root'
})

export class AuthorizationCheckService{
    private isCookieExist: boolean | null;
    public cookieEvent = new Subject();

    constructor(private router: Router){  }
    public checkAuthorization(): boolean{
        this.isCookieExist = document.cookie.match(/Cookie=/) !== null;
        if(this.isCookieExist){
            return true;     
        }
        return false;
    }
}