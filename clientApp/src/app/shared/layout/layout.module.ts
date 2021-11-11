import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { HeaderComponent } from "./header/header.component";
import { MatButtonModule } from "@angular/material/button";
import { FooterComponent } from "./footer/footer.component";
import { MainLayoutComponent } from "./main-layout/main-layout.component";
import { CommonModule } from "@angular/common";
@NgModule({
    imports: [
        RouterModule.forChild([]),
        MatButtonModule,
        CommonModule
    ],
    declarations:[
        MainLayoutComponent,
        HeaderComponent,
        FooterComponent
    ],
    exports: [
        MainLayoutComponent,
        
    ]
})

export class LayoutModule { }