import { Component, OnInit} from "@angular/core";
import { AuthEventService } from "../../service/authorization-event.service";

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit{
    public isHideButtons: boolean;
    constructor(private authEvent: AuthEventService){
        this.isAuthorized();        
    }
    ngOnInit(){
        this.authEvent.atAuthorized.subscribe(e => this.isAuthorized());
    }
    public isAuthorized(){

          this.isHideButtons = document.cookie.match(/Cookie=/) !== null;
    }
}