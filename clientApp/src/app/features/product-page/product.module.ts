import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";
import { ProductComponent } from "./product.component";
import { MatTableModule } from "@angular/material/table";
import { MatButtonModule } from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import { ProductService } from "./service/product.service";
@NgModule({
    imports:[
        BrowserModule,
        FormsModule,
        MatTableModule,
        MatButtonModule,
        MatIconModule
    ],
    declarations:[
        ProductComponent
    ],
    providers: [ProductService]
})

export class ProductModule { }