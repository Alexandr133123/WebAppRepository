import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { FilterEventService } from "src/app/features/product-page/service/filter-event.service";

@Component({
    selector: 'product-IOOS-comp',
    templateUrl: './product-IOOS.component.html'
})

export class IncludeOutOfStockComponent implements OnInit{
    @Output() sendOutOfStock = new EventEmitter<boolean>();
    public includeOutOfStock: boolean = false;
    
    constructor(private filterEvent: FilterEventService){
    }

    ngOnInit(){
        this.filterEvent.searchInvoked.subscribe(e => this.sendOutOfStock.emit(this.includeOutOfStock));
    }

}