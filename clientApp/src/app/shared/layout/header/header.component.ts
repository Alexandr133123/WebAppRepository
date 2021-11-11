import { Component, OnInit} from "@angular/core";
import { AuthorizationCheckService } from "../../service/authorization-check.service";

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.css']
})
export class HeaderComponent{
    public isHideButtons: boolean;
    constructor( private authCheck: AuthorizationCheckService){
        this.isAuthorized();
        this.authCheck.cookieEvent.subscribe(e => this.isAuthorized());
    }
    public isAuthorized(){
          this.isHideButtons = this.authCheck.checkAuthorization();
    }
}