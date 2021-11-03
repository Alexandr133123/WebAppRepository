import { Component, Input, Output } from "@angular/core";
import { Product } from "src/app/shared/models/Product";
import { NgForm } from "@angular/forms";
import { MatTableDataSource } from "@angular/material/table";
import { EventEmitter } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { AddProductComponent } from "../add-product/add-product.component";
import { ProductService } from "../../service/product.service";

@Component({
    selector: 'Xproduct-info',
    templateUrl: './product-info.component.html',
    styleUrls: ['./product-info.component.scss']
})

export class ProductInfoComponent {
    @Input() product: Product;
    @Output() sendProductId = new EventEmitter<number>();

    constructor(public dialog: MatDialog, private productService: ProductService){}

    public openDialog(){
        const dialogRef = this.dialog.open(AddProductComponent, {
            data: this.product
        });
        dialogRef.afterClosed().subscribe((result: Product) => {
            if(result){                
                this.productService.putEditedProduct(result).subscribe();
            }
        });
    }

    public delete(productId: number) {
        this.sendProductId.emit(productId);
    }
}