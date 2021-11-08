import { Subject } from "rxjs";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class EventService {
    public searchInvoked = new Subject();
    public productLoadInvoked = new Subject();
}