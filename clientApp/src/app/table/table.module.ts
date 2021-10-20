import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";
import { TableComponent } from "./table.component";
import { MatTableModule } from "@angular/material/table";
import { MatButtonModule } from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
@NgModule({
    imports:[
        BrowserModule,
        FormsModule,
        MatTableModule,
        MatButtonModule,
        MatIconModule
    ],
    declarations:[
        TableComponent
    ],
    exports:[
        TableComponent
    ]
})

export class TableModule{}