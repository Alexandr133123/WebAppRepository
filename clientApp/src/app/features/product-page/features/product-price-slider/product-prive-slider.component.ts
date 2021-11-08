import { Component, Input, OnInit, Output } from "@angular/core";
import { EventEmitter } from "@angular/core";
import { EventService } from "src/app/features/product-page/service/event.service";


@Component({
    selector: 'product-price-slider-comp',
    templateUrl: './product-price-slider.component.html'
})

export class ProductPriceSliderComponent implements OnInit {
    @Input() maxProductPrice: number;
    @Output() sendProductPriceFilter = new EventEmitter<number>();
    public currentPriceFilter: number;

    constructor(private filterEvent: EventService) { }

    public ngOnInit() {
        this.filterEvent.searchInvoked.subscribe(e => this.sendProductPriceFilter.emit(this.currentPriceFilter));
    }

}