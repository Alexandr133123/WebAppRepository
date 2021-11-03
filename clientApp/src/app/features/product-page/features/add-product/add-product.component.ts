import { Component, Inject } from "@angular/core";
import { FormControl } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Category } from "src/app/shared/models/Category";
import { Product } from "src/app/shared/models/Product";


@Component({
    selector: 'add-product-comp',
    templateUrl: './add-product.component.html',
    styleUrls: ['./add-product.component.scss']
})

export class AddProductComponent {

    public formControl = new FormControl();
    constructor(public dialogRef: MatDialogRef<AddProductComponent>, @Inject(MAT_DIALOG_DATA) public data: Product) { }

    public setSelectedCategories(categories: Category[]){
        this.data.categories = categories;
    }

    public close(): void {
        console.log(this.data.categories);
        this.dialogRef.close();
    }

}