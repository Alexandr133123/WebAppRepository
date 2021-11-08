import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { EventService } from "src/app/features/product-page/service/event.service";

@Component({
    selector: 'product-IOOS-comp',
    templateUrl: './product-IOOS.component.html'
})

export class IncludeOutOfStockComponent implements OnInit {
    @Output() sendOutOfStock = new EventEmitter<boolean>();
    public includeOutOfStock: boolean = false;

    constructor(private filterEvent: EventService) {
    }

    public ngOnInit() {
        this.filterEvent.searchInvoked.subscribe(e => this.sendOutOfStock.emit(this.includeOutOfStock));
    }

}