import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { LoginPageComponent } from "./login-page.component";
import { MatStepperModule} from "@angular/material/stepper";
import { MatButtonModule } from "@angular/material/button";
import {MatFormFieldModule} from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
@NgModule({
    imports:[
        FormsModule,
        BrowserModule,
        MatStepperModule,
        MatButtonModule,
        MatFormFieldModule,
        ReactiveFormsModule,
        MatInputModule
    ],
    declarations: [
        LoginPageComponent
    ],
})

export class LoginPageModule {

}