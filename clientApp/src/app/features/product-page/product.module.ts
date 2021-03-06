import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { ProductComponent } from "./product.component";
import {MatCardModule} from "@angular/material/card";
import { ProductInfoComponent } from "./features/product-info/product-info.component";
import { MatButtonModule } from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import { MatFormFieldModule } from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import { MatTreeModule } from "@angular/material/tree";
import {MatCheckboxModule} from "@angular/material/checkbox"
import {MatSliderModule} from '@angular/material/slider';
import { ProductSearchInputComponent } from "./features/product-search-input/product-search-input.component";
import { ProductPriceSliderComponent } from "./features/product-price-slider/product-prive-slider.component";
import { IncludeOutOfStockComponent } from "./features/product-includeOutOfStock-checkbox/product-IOOS.components";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatTableModule } from "@angular/material/table";
import { MatProgressSpinnerModule} from "@angular/material/progress-spinner"
import { AddProductComponent } from "./features/add-product/add-product.component";
import {MatDialogModule} from "@angular/material/dialog";
import { MatSelectModule } from "@angular/material/select";
import { CategoryInfoModule } from "src/app/shared/category-info/category-info.module";
@NgModule({
    imports:[
        BrowserModule,
        FormsModule,
        MatCardModule,
        MatButtonModule,
        MatIconModule,
        MatFormFieldModule,
        MatInputModule,
        MatCheckboxModule,
        MatSliderModule,
        MatPaginatorModule,
        MatTableModule,
        MatProgressSpinnerModule,
        MatDialogModule,
        MatSelectModule,
        CategoryInfoModule,
        ReactiveFormsModule,
        
    ],
    declarations:[
        ProductComponent,
        ProductInfoComponent,
        ProductSearchInputComponent,
        ProductPriceSliderComponent,
        IncludeOutOfStockComponent,
        AddProductComponent
    
    ],
    
})

export class ProductModule { }