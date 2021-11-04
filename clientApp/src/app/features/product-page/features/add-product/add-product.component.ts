import { Component, Inject } from "@angular/core";
import { FormArray, FormControl, FormGroup, Validators } from "@angular/forms";
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
    public isNotSelected: boolean;
    public productForm: FormGroup;
    constructor(public dialogRef: MatDialogRef<AddProductComponent>, @Inject(MAT_DIALOG_DATA) public data: Product) {        
        this.productForm = new FormGroup({
            "productName": new FormControl(data?.productName,Validators.required, ),
            "price": new FormControl(data?.price,[Validators.required, Validators.pattern(/^(0?\.\d*[1-9]|[1-9]\d*(\.\d*[1-9])?)$/)]),
            "quantityInStock": new FormControl(data?.quantityInStock,[Validators.required, Validators.pattern(/^(0|[^0\D](\d)*)$/)]),
        });
     }
     public isCategoriesSelected(category: Category[]){
        if(category){
            this.data.categories = category;
            this.isNotSelected = false;
        }else{
            this.isNotSelected = true;
        }
     }
    public setSelectedCategories(){  
        if(this.data.categories != undefined && this.data.categories.length){
            console.log(this.data.categories);
            this.data.productName = this.productForm.controls['productName'].value;
        this.data.price = this.productForm.controls['price'].value;
        this.data.quantityInStock = this.productForm.controls['quantityInStock'].value;   
        this.dialogRef.close(this.data);
        }else{            
            this.isNotSelected = true;
        }
    }

    public close(): void {
        console.log(this.data.categories);
        this.dialogRef.close();
    }

}