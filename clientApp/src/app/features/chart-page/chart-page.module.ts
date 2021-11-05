import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { NgApexchartsModule } from "ng-apexcharts";
import { ChartPageComponent } from "./chart-page.component";
import { MatButtonModule } from "@angular/material/button";
import {MatButtonToggleModule} from "@angular/material/button-toggle";

@NgModule({
    imports: [
        BrowserModule,
        NgApexchartsModule,
        MatButtonToggleModule
    ],
    declarations: [
        ChartPageComponent
    ]
})
export class ChartPageModule { }