import { Component, Input, Output } from "@angular/core";
import { Product } from "src/app/shared/models/Product";
import { NgForm } from "@angular/forms";
import { MatTableDataSource } from "@angular/material/table";
import { EventEmitter } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { AddProductComponent } from "../add-product/add-product.component";
import { ProductService } from "../../../../shared/service/product.service";
import { ProductWithImageInfo } from "../../models/ProductWithImageInfo";
import { EventService } from "../../service/event.service";

@Component({
    selector: 'Xproduct-info',
    templateUrl: './product-info.component.html',
    styleUrls: ['./product-info.component.scss']
})

export class ProductInfoComponent {
    @Input() product: Product;
    @Output() sendProductId = new EventEmitter<number>();

    constructor(public dialog: MatDialog, private productService: ProductService, private eventService: EventService){}

    public openDialog(){
        const dialogRef = this.dialog.open(AddProductComponent, {
            data: this.product
        });
        dialogRef.afterClosed().subscribe((result: ProductWithImageInfo) => {
            if(result){                
                this.productService.updateProduct(result.product,result.file).subscribe();
                this.eventService.productLoadInvoked.next();
            }
        });
    }

    public delete(productId: number) {
        this.sendProductId.emit(productId);
    }
}