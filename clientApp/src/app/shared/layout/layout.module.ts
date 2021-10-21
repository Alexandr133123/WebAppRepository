import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { HeaderComponent } from "./header/header.component";
import { MatButtonModule } from "@angular/material/button";
@NgModule({
    imports: [
        RouterModule.forChild([]),
        MatButtonModule
    ],
    declarations: [
        HeaderComponent
    ],
    exports: [
        HeaderComponent,
        
    ]
})

export class LayoutModule { }