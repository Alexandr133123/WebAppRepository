import { NgModule } from "@angular/core";
import { CategoryInfoComponent } from "./category-info.component";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from "@angular/material/button";
import { MatTreeModule, MatTreeNode } from "@angular/material/tree";
import { FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";

@NgModule({

    imports: [MatCheckboxModule,
        FormsModule,
        BrowserModule,
        MatIconModule, 
        MatButtonModule, 
        MatTreeModule],
    declarations: [CategoryInfoComponent],
    exports:[CategoryInfoComponent]
})

export class CategoryInfoModule {}