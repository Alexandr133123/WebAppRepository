import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";
import { ProductComponent } from "./product.component";
import {MatCardModule} from "@angular/material/card";
import { ProductInfoComponent } from "./features/product-info/product-info.component";
import { MatButtonModule } from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import { MatFormFieldModule } from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import { CategorySidebarInfoComponent } from "./features/category-sidebar-info/category-sidebar-info.component";
import { MatTreeModule } from "@angular/material/tree";
import {MatCheckboxModule} from "@angular/material/checkbox"
import {MatSliderModule} from '@angular/material/slider';
import { ProductSearchInputComponent } from "./features/product-search-input/product-search-input.component";
import { ProductPriceSliderComponent } from "./features/category-sidebar-info/features/product-price-slider/product-prive-slider.component";
import { IncludeOutOfStockComponent } from "./features/category-sidebar-info/features/product-includeOutOfStock-checkbox/product-IOOS.components";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatTableModule } from "@angular/material/table";
import { MatProgressSpinnerModule} from "@angular/material/progress-spinner"
import { AddProductComponent } from "./features/add-product/add-product.component";
import {MatDialogModule} from "@angular/material/dialog";
import { MatSelectModule } from "@angular/material/select";
import { MdePopoverModule } from "@material-extended/mde";
@NgModule({
    imports:[
        BrowserModule,
        FormsModule,
        MatCardModule,
        MatButtonModule,
        MatIconModule,
        MatFormFieldModule,
        MatInputModule,
        MatTreeModule,
        MatCheckboxModule,
        MatSliderModule,
        MatPaginatorModule,
        MatTableModule,
        MatProgressSpinnerModule,
        MatDialogModule,
        MatSelectModule,
        MdePopoverModule
    ],
    declarations:[
        ProductComponent,
        ProductInfoComponent,
        CategorySidebarInfoComponent,
        ProductSearchInputComponent,
        ProductPriceSliderComponent,
        IncludeOutOfStockComponent,
        AddProductComponent
    
    ],
    
})

export class ProductModule { }