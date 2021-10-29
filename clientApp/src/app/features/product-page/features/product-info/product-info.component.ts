import { Component, Input, Output } from "@angular/core";
import { Product } from "src/app/shared/models/Product";
import { NgForm } from "@angular/forms";
import { MatTableDataSource } from "@angular/material/table";
import { EventEmitter } from "@angular/core";

@Component({
    selector: 'Xproduct-info',
    templateUrl: './product-info.component.html'
})

export class ProductInfoComponent {
    @Input() product: Product;
    @Output() sendProductId = new EventEmitter<string>();

    public delete(productName: string) {
        this.sendProductId.emit(productName);
    }
}