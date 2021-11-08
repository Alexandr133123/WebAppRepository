import { Component, EventEmitter, Inject, Output } from "@angular/core";
import { FormArray, FormControl, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { first } from "rxjs/operators";
import { Category } from "src/app/shared/models/Category";
import { Product } from "src/app/shared/models/Product";
import { ProductService } from "src/app/shared/service/product.service";
import { ProductWithImageInfo } from "../../models/ProductWithImageInfo";

@Component({
    selector: 'add-product-comp',
    templateUrl: './add-product.component.html',
    styleUrls: ['./add-product.component.scss']
})

export class AddProductComponent {

    public formControl = new FormControl();
    public isNotSelected: boolean;
    public productForm: FormGroup;
    public uploadedFile: File;
    public imageUrl: string;
    private dialogResponse: ProductWithImageInfo;
    constructor(public dialogRef: MatDialogRef<AddProductComponent>, @Inject(MAT_DIALOG_DATA) public data: Product) {     
        this.dialogResponse = new ProductWithImageInfo();
        if(data.image){
            this.imageUrl = 'data:image/jpg;base64,' + this.data.image;
        }   
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
        this.dialogResponse.product = this.data;
        this.dialogResponse.file = this.uploadedFile;      
        this.dialogRef.close(this.dialogResponse);

        }else{            
            this.isNotSelected = true;
        }
    }

    public setChoosenFile(file: FileList){
        this.uploadedFile = file.item(0)!;
        var base64String = ""; 
        this.uploadedFile.arrayBuffer().then(e => {
            base64String = this.arrayBufferToBase64(e);
            this.imageUrl = "data:image/jpeg;base64," + base64String; 
        });

        
    }

    private arrayBufferToBase64( buffer: ArrayBuffer ) {
        var binary = "";
        var bytes = new Uint8Array( buffer );
        var len = bytes.byteLength;
        for (var i = 0; i < len; i++) {
            binary += String.fromCharCode( bytes[ i ] );
        }
        return window.btoa(binary);
    }


    public close(): void {
        console.log(this.data.categories);
        this.dialogRef.close();
    }

}