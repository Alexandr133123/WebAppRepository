import { Component, Input, OnInit, Output } from "@angular/core";
import { EventEmitter } from "@angular/core";
import { FilterEventService } from "src/app/features/product-page/service/filter-event.service";


@Component({
    selector: 'product-price-slider-comp',
    templateUrl: './product-price-slider.component.html'
})

export class ProductPriceSliderComponent implements OnInit{
    @Input() maxProductPrice: number;
    @Output() sendProductPriceFilter = new EventEmitter<number>();
    public currentPriceFilter: number;

    constructor(private filterEvent: FilterEventService){}

    ngOnInit(){
        this.filterEvent.searchInvoked.subscribe(e => this.sendProductPriceFilter.emit(this.currentPriceFilter)).add(() => alert(this.currentPriceFilter));
    }

}