import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";
import { DownloadComponent } from "./download.component";
import { CategoryInfoModule } from "src/app/shared/category-info/category-info.module";
import {MatDatepickerModule} from "@angular/material/datepicker";
import { MatNativeDateModule } from "@angular/material/core";
import { MatInputModule } from "@angular/material/input";
import { MatButtonModule } from "@angular/material/button";
import {MatRadioModule} from "@angular/material/radio";
import { MatCheckboxModule } from "@angular/material/checkbox";

@NgModule({
    imports: [
        BrowserModule, 
        FormsModule, 
        CategoryInfoModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatInputModule,
        MatButtonModule,
        MatRadioModule,
        MatCheckboxModule
    ],
    declarations: [DownloadComponent],

})

export class DownloadModule { }