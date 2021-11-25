import { Subject } from "rxjs";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class AuthEventService {
    public atAuthorized = new Subject();
}